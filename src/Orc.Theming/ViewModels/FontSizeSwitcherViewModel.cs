namespace Orc.Theming.ViewModels;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catel.MVVM;

public class FontSizeSwitcherViewModel : ViewModelBase
{
    private readonly IFontSizeService _fontSizeService;

    public FontSizeSwitcherViewModel(IFontSizeService fontSizeService)
    {
        ArgumentNullException.ThrowIfNull(fontSizeService);

        _fontSizeService = fontSizeService;

        var fontSizes = new List<double>(new[]
        {
            10d,
            11d,
            12d,
            14d,
            16d,
            18d,
            20d,
            22d,
            24d
        });

        var currentFontSize = fontSizeService.GetFontSize();
        if (!fontSizes.Contains(currentFontSize))
        {
            fontSizes.Insert(0, currentFontSize);
        }

        FontSizes = fontSizes;
        SelectedFontSize = currentFontSize;
    }

    public List<double> FontSizes { get; }

    public double SelectedFontSize { get; set; }

    protected override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        _fontSizeService.FontSizeChanged += OnFontSizeServiceFontSizeChanged;
    }

    protected override async Task CloseAsync()
    {
        _fontSizeService.FontSizeChanged -= OnFontSizeServiceFontSizeChanged;

        await base.CloseAsync();
    }

    private void OnFontSizeServiceFontSizeChanged(object? sender, EventArgs e)
    {
        SelectedFontSize = _fontSizeService.GetFontSize();
    }

    private void OnSelectedFontSizeChanged()
    {
        _fontSizeService.SetFontSize(SelectedFontSize);
    }
}