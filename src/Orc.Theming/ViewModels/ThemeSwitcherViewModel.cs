namespace Orc.Theming.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Media;
    using Catel;
    using Catel.MVVM;
    using Catel.Reflection;

    public class ThemeSwitcherViewModel : ViewModelBase
    {
        private readonly IAccentColorService _accentColorService;
        private readonly IBaseColorSchemeService _baseColorSchemeService;

        public ThemeSwitcherViewModel(IAccentColorService accentColorService, IBaseColorSchemeService baseColorSchemeService)
        {
            Argument.IsNotNull(() => accentColorService);
            Argument.IsNotNull(() => baseColorSchemeService);

            _accentColorService = accentColorService;
            _baseColorSchemeService = baseColorSchemeService;

            AccentColors = typeof(Colors).GetPropertiesEx(true, true)
                .Where(x => x.PropertyType.IsAssignableFromEx(typeof(Color)))
                .Select(x => (Color)x.GetValue(null))
                .ToList();

            BaseColorSchemes = _baseColorSchemeService.GetAvailableBaseColorSchemes();
            SelectedBaseColorScheme = _baseColorSchemeService.GetBaseColorScheme() ?? BaseColorSchemes[0];
        }

        public List<Color> AccentColors { get; }
        public IReadOnlyList<string> BaseColorSchemes { get; }

        public Color SelectedAccentColor { get; set; }
        public string SelectedBaseColorScheme { get; set; }

        private void OnSelectedAccentColorChanged()
        {
            _accentColorService.SetAccentColor(SelectedAccentColor);
        }

        private void OnSelectedBaseColorSchemeChanged()
        {
            _baseColorSchemeService.SetBaseColorScheme(SelectedBaseColorScheme);
        }
    }
}
