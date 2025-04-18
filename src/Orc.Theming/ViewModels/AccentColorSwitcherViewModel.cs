namespace Orc.Theming.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using Catel.MVVM;
using Catel.Reflection;

public class AccentColorSwitcherViewModel : ViewModelBase
{
    private readonly IAccentColorService _accentColorService;

    public AccentColorSwitcherViewModel(ThemeManager themeManager, IAccentColorService accentColorService)
    {
        ArgumentNullException.ThrowIfNull(accentColorService);

        _accentColorService = accentColorService;

        ValidateUsingDataAnnotations = false;

        AccentColors = typeof(Colors).GetPropertiesEx(true, true)
            .Where(x => x.PropertyType.IsAssignableFromEx(typeof(Color)))
            .Select(x => (Color?)x.GetValue(null))
            .Where(x => x is not null)
            .Cast<Color>()
            .ToList();

        var currentAccentColor = themeManager.GetAccentColorBrush().Color;
        if (!AccentColors.Contains(currentAccentColor))
        {
            AccentColors.Insert(0, currentAccentColor);
        }

        SelectedAccentColor = currentAccentColor;
    }

    public List<Color> AccentColors { get; }

    public Color SelectedAccentColor { get; set; }

    protected override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        _accentColorService.AccentColorChanged += OnAccentColorServiceAccentColorChanged;
    }

    protected override async Task CloseAsync()
    {
        _accentColorService.AccentColorChanged -= OnAccentColorServiceAccentColorChanged;

        await base.CloseAsync();
    }

    private void OnAccentColorServiceAccentColorChanged(object? sender, EventArgs e)
    {
        SelectedAccentColor = _accentColorService.GetAccentColor();
    }

    private void OnSelectedAccentColorChanged()
    {
        _accentColorService.SetAccentColor(SelectedAccentColor);
    }
}
