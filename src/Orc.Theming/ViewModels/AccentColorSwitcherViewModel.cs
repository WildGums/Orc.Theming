namespace Orc.Theming.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Media;
    using Catel;
    using Catel.MVVM;
    using Catel.Reflection;

    public class AccentColorSwitcherViewModel : ViewModelBase
    {
        private readonly IAccentColorService _accentColorService;

        public AccentColorSwitcherViewModel(ThemeManager themeManager, IAccentColorService accentColorService)
        {
            Argument.IsNotNull(() => accentColorService);

            _accentColorService = accentColorService;

            var accentColors = typeof(Colors).GetPropertiesEx(true, true).Where(x => x.PropertyType.IsAssignableFromEx(typeof(Color))).Select(x => (Color)x.GetValue(null)).ToList();

            var currentAccentColor = themeManager.GetAccentColorBrush()?.Color ?? Colors.Transparent;
            if (!accentColors.Contains(currentAccentColor))
            {
                accentColors.Insert(0, currentAccentColor);
            }

            AccentColors = accentColors;
            SelectedAccentColor = themeManager.GetThemeColor();
        }

        public List<Color> AccentColors { get; private set; }

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

        private void OnAccentColorServiceAccentColorChanged(object sender, EventArgs e)
        {
            SelectedAccentColor = _accentColorService.GetAccentColor();
        }

        private void OnSelectedAccentColorChanged()
        {
            _accentColorService.SetAccentColor(SelectedAccentColor);
        }
    }
}
