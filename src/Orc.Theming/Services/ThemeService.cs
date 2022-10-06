namespace Orc.Theming
{
    using System;

    public class ThemeService : IThemeService
    {
        private readonly IAccentColorService _accentColorService;
        private readonly IBaseColorSchemeService _baseColorSchemeService;

        public ThemeService(IAccentColorService accentColorService, IBaseColorSchemeService baseColorSchemeService)
        {
            ArgumentNullException.ThrowIfNull(accentColorService);
            ArgumentNullException.ThrowIfNull(baseColorSchemeService);

            _accentColorService = accentColorService;
            _baseColorSchemeService = baseColorSchemeService;
        }

        public virtual bool ShouldCreateStyleForwarders()
        {
            return true;
        }

        public virtual ThemeInfo GetThemeInfo()
        {
            var accentColor = _accentColorService.GetAccentColor();

            var themeInfo = new ThemeInfo
            {
                BaseColorScheme = _baseColorSchemeService.GetBaseColorScheme(), 
                AccentBaseColor = accentColor,
                HighlightColor = accentColor
            };

            return themeInfo;
        }
    }
}
