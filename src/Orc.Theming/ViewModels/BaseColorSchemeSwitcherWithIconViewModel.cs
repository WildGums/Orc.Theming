namespace Orc.Theming.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catel.IoC;
using Catel.MVVM;

public class BaseColorSchemeSwitcherWithIconViewModel : ViewModelBase
{
    private readonly IBaseColorSchemeService _baseColorSchemeService;

#pragma warning disable CA1801 // Review unused parameters
    public BaseColorSchemeSwitcherWithIconViewModel(IBaseColorSchemeService baseColorSchemeService)
#pragma warning restore CA1801 // Review unused parameters
    {
        ArgumentNullException.ThrowIfNull(baseColorSchemeService);

        _baseColorSchemeService = baseColorSchemeService;

        ValidateUsingDataAnnotations = false;

        var baseColorSchemes = _baseColorSchemeService.GetAvailableBaseColorSchemes()
            .Select(baseColorSchemeFromService => new BaseColorScheme
            {
                Name = baseColorSchemeFromService,
                ImageUri = $"/Orc.Theming;component/Resources/Images/BaseColor_{baseColorSchemeFromService}.png"
            })
            .ToList();

        BaseColorSchemes = baseColorSchemes;

        var selected = _baseColorSchemeService.GetBaseColorScheme();

        SelectedBaseColorScheme = BaseColorSchemes.FirstOrDefault(x => x.Name == selected);
    }

    public IReadOnlyList<BaseColorScheme> BaseColorSchemes { get; }

    public BaseColorScheme? SelectedBaseColorScheme { get; set; }

    protected override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        _baseColorSchemeService.BaseColorSchemeChanged += OnBaseColorSchemeServiceBaseColorSchemeChanged;
    }

    protected override async Task CloseAsync()
    {
        _baseColorSchemeService.BaseColorSchemeChanged -= OnBaseColorSchemeServiceBaseColorSchemeChanged;

        await base.CloseAsync();
    }

    private void OnBaseColorSchemeServiceBaseColorSchemeChanged(object? sender, EventArgs e)
    {
        var newlySelected = _baseColorSchemeService.GetBaseColorScheme();

        SelectedBaseColorScheme = BaseColorSchemes.FirstOrDefault(x => x.Name == newlySelected);
    }

    private async void OnSelectedBaseColorSchemeChanged()
    {
        var selected = SelectedBaseColorScheme;
        if (selected is null)
        {
            return;
        }

        // Note: short delay to allow some animation
        await Task.Delay(200);

        _baseColorSchemeService.SetBaseColorScheme(selected.Name);
    }
}
