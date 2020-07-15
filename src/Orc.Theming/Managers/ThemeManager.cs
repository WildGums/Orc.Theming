namespace Orc.Theming
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using Catel;
    using Catel.Caching;
    using Catel.IoC;
    using Catel.Logging;
    using ControlzEx.Theming;
    using MethodTimer;

    public class ThemeManager
    {
        #region Constructors
        public ThemeManager(ControlzEx.Theming.ThemeManager controlzThemeManager, IAccentColorService accentColorService, IBaseColorSchemeService baseColorSchemeService)
        {
            Argument.IsNotNull(() => controlzThemeManager);
            Argument.IsNotNull(() => accentColorService);
            Argument.IsNotNull(() => baseColorSchemeService);

            _accentColorService = accentColorService;
            _baseColorSchemeService = baseColorSchemeService;
            _controlzThemeManager = controlzThemeManager;

            _controlzThemeManager.ThemeChanged += OnThemeManagerThemeChanged;
            _accentColorService.AccentColorChanged += OnAccentColorChanged;
            _baseColorSchemeService.BaseColorSchemeChanged += OnBaseColorSchemeChanged;
        }
        #endregion

        public static ThemeManager Current { get { return CurrentLazy.Value; } }

        public event EventHandler<EventArgs> ThemeChanged;

        #region Fields
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly ControlzEx.Theming.ThemeManager _controlzThemeManager;
        private readonly IAccentColorService _accentColorService;
        private readonly IBaseColorSchemeService _baseColorSchemeService;

        private readonly CacheStorage<string, SolidColorBrush> _resourceBrushesCache = new CacheStorage<string, SolidColorBrush>();

        private readonly CacheStorage<ThemeColorStyle, Color> _themeColorsCache = new CacheStorage<ThemeColorStyle, Color>();
        private readonly CacheStorage<ThemeColorStyle, SolidColorBrush> _themeColorBrushesCache = new CacheStorage<ThemeColorStyle, SolidColorBrush>();

        private SolidColorBrush _accentColorBrushCache;

        private Theme _currentTheme;

        // Note: must be lazy because we don't want the static ctor to be invoked whenever we resolve this class correctly via DI
        private static readonly Lazy<ThemeManager> CurrentLazy = new Lazy<ThemeManager>(() => ServiceLocator.Default.ResolveType<ThemeManager>());
        #endregion

        #region Methods
        public Color GetThemeColor(string resourceName)
        {
            var resource = _currentTheme?.Resources[resourceName];
            if (resource is Color color)
            {
                return color;
            }

            return Colors.Transparent;
        }

        public SolidColorBrush GetThemeColorBrush(string resourceName)
        {
            return _resourceBrushesCache.GetFromCacheOrFetch(resourceName, () =>
            {
                var color = GetThemeColor(resourceName);
                return color.ToSolidColorBrush();
            });
        }

        public Color GetThemeColor(ThemeColorStyle colorStyle = ThemeColorStyle.AccentColor)
        {
            return _themeColorsCache.GetFromCacheOrFetch(colorStyle, () =>
            {
                switch (colorStyle)
                {
                    // Accent color
                    case ThemeColorStyle.AccentColor:
                        return GetThemeColor("Orc.Colors.AccentColor");

                    case ThemeColorStyle.AccentColor80:
                        return GetThemeColor("Orc.Colors.AccentColor80");

                    case ThemeColorStyle.AccentColor60:
                        return GetThemeColor("Orc.Colors.AccentColor60");

                    case ThemeColorStyle.AccentColor40:
                        return GetThemeColor("Orc.Colors.AccentColor40");

                    case ThemeColorStyle.AccentColor20:
                        return GetThemeColor("Orc.Colors.AccentColor20");

                    // Border
                    case ThemeColorStyle.BorderColor:
                        return GetThemeColor("Orc.Colors.Control.Border");

                    // Background
                    case ThemeColorStyle.BackgroundColor:
                        return GetThemeColor("Orc.Colors.Background");

                    // Foreground
                    case ThemeColorStyle.ForegroundColor:
                        return GetThemeColor("Orc.Colors.Foreground");

                    // Highlight
                    case ThemeColorStyle.HighlightColor:
                        return GetThemeColor("Orc.Colors.HighlightColor");

                    // Gray
                    case ThemeColorStyle.Gray1:
                        return GetThemeColor("Gray1");

                    case ThemeColorStyle.Gray2:
                        return GetThemeColor("Gray2");

                    case ThemeColorStyle.Gray3:
                        return GetThemeColor("Gray3");

                    case ThemeColorStyle.Gray4:
                        return GetThemeColor("Gray4");

                    case ThemeColorStyle.Gray5:
                        return GetThemeColor("Gray5");

                    case ThemeColorStyle.Gray6:
                        return GetThemeColor("Gray6");

                    case ThemeColorStyle.Gray7:
                        return GetThemeColor("Gray7");

                    case ThemeColorStyle.Gray8:
                        return GetThemeColor("Gray8");

                    case ThemeColorStyle.Gray9:
                        return GetThemeColor("Gray9");

                    case ThemeColorStyle.Gray10:
                        return GetThemeColor("Gray10");

                    // Text
                    case ThemeColorStyle.Text:
                        return GetThemeColor("Orc.Colors.Text");

                    default:
                        throw new ArgumentOutOfRangeException(nameof(colorStyle));
                }
            });
        }

        public SolidColorBrush GetThemeColorBrush(ThemeColorStyle colorStyle = ThemeColorStyle.AccentColor)
        {
            return _themeColorBrushesCache.GetFromCacheOrFetch(colorStyle, () =>
            {
                var color = GetThemeColor(colorStyle);
                return color.ToSolidColorBrush();
            });
        }

        public SolidColorBrush GetAccentColorBrush()
        {
            if (_accentColorBrushCache is null)
            {
                _accentColorBrushCache = _currentTheme?.PrimaryAccentColor.ToSolidColorBrush() ?? Application.Current?.TryFindResource("AccentColorBrush") as SolidColorBrush ?? Brushes.Green;
            }

            return _accentColorBrushCache;
        }

        private void OnThemeManagerThemeChanged(object sender, ThemeChangedEventArgs e)
        {
            Log.Debug("Theme has changed, clearing current cache");

            _accentColorBrushCache = null;
            _resourceBrushesCache.Clear();
            _themeColorBrushesCache.Clear();
            _themeColorsCache.Clear();

            _currentTheme = e.NewTheme;
        }

        private void OnAccentColorChanged(object sender, EventArgs e)
        {
            SynchronizeTheme();
        }

        private void OnBaseColorSchemeChanged(object sender, EventArgs e)
        {
            SynchronizeTheme();
        }

        public virtual void SynchronizeTheme()
        {
            Log.Debug("Synchronizing theme");

            var themeGenerator = RuntimeThemeGenerator.Current;

            var generatedTheme = themeGenerator.GenerateRuntimeTheme(_baseColorSchemeService.GetBaseColorScheme(), _accentColorService.GetAccentColor());
            if (generatedTheme is null)
            {
                throw Log.ErrorAndCreateException<InvalidOperationException>("Failed to generate runtime theme");
            }

            ChangeTheme(generatedTheme);
        }

        [Time]
        private void ChangeTheme(Theme generatedTheme)
        {
            _controlzThemeManager.ChangeTheme(Application.Current, generatedTheme);

            ThemeChanged?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
