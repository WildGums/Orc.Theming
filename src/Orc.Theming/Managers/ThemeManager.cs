namespace Orc.Theming;

using System;
using System.Windows;
using System.Windows.Media;
using Catel.Caching;
using Catel.IoC;
using Catel.Logging;
using ControlzEx.Theming;
using MethodTimer;

public class ThemeManager
{
    private static readonly ILog Log = LogManager.GetCurrentClassLogger();

    private readonly ControlzEx.Theming.ThemeManager _controlzThemeManager;
    private readonly IAccentColorService _accentColorService;
    private readonly IBaseColorSchemeService _baseColorSchemeService;

    private readonly CacheStorage<string, SolidColorBrush> _resourceBrushesCache = new();
    private readonly CacheStorage<ThemeColorStyle, Color> _themeColorsCache = new();
    private readonly CacheStorage<ThemeColorStyle, SolidColorBrush> _themeColorBrushesCache = new();

    private SolidColorBrush? _accentColorBrushCache;

    private Theme? _currentTheme;

    // Note: must be lazy because we don't want the static ctor to be invoked whenever we resolve this class correctly via DI
    private static readonly Lazy<ThemeManager> CurrentLazy = new(() => ServiceLocator.Default.ResolveRequiredType<ThemeManager>());

    public ThemeManager(ControlzEx.Theming.ThemeManager controlzThemeManager, IAccentColorService accentColorService, IBaseColorSchemeService baseColorSchemeService)
    {
        ArgumentNullException.ThrowIfNull(controlzThemeManager);
        ArgumentNullException.ThrowIfNull(accentColorService);
        ArgumentNullException.ThrowIfNull(baseColorSchemeService);

        _accentColorService = accentColorService;
        _baseColorSchemeService = baseColorSchemeService;
        _controlzThemeManager = controlzThemeManager;

        _controlzThemeManager.ThemeChanged += OnThemeManagerThemeChanged;
        _accentColorService.AccentColorChanged += OnAccentColorChanged;
        _baseColorSchemeService.BaseColorSchemeChanged += OnBaseColorSchemeChanged;

        SynchronizeTheme();
    }

    public static ThemeManager Current { get { return CurrentLazy.Value; } }

    public event EventHandler<EventArgs>? ThemeChanged;

    public Color GetThemeColor(string resourceName)
    {
        var resource = _currentTheme?.Resources[resourceName];
        if (resource is Color color)
        {
            return color;
        }

        return Colors.Transparent;
    }

    public Color GetThemeColor(ThemeColorStyle colorStyle = ThemeColorStyle.AccentColor)
    {
        return _themeColorsCache.GetFromCacheOrFetch(colorStyle, () =>
        {
            return colorStyle switch
            {
                // Accent color
                ThemeColorStyle.AccentColor => GetThemeColor("Orc.Colors.AccentColor"),
                ThemeColorStyle.AccentColor80 => GetThemeColor("Orc.Colors.AccentColor80"),
                ThemeColorStyle.AccentColor60 => GetThemeColor("Orc.Colors.AccentColor60"),
                ThemeColorStyle.AccentColor40 => GetThemeColor("Orc.Colors.AccentColor40"),
                ThemeColorStyle.AccentColor20 => GetThemeColor("Orc.Colors.AccentColor20"),
                // Border
                ThemeColorStyle.BorderColor => GetThemeColor("Orc.Colors.Control.Border"),
                // Background
                ThemeColorStyle.BackgroundColor => GetThemeColor("Orc.Colors.Background"),
                // Foreground
                ThemeColorStyle.ForegroundColor => GetThemeColor("Orc.Colors.Foreground"),
                // Highlight
                ThemeColorStyle.HighlightColor => GetThemeColor("Orc.Colors.HighlightColor"),
                // Gray
                ThemeColorStyle.Gray1 => GetThemeColor("Gray1"),
                ThemeColorStyle.Gray2 => GetThemeColor("Gray2"),
                ThemeColorStyle.Gray3 => GetThemeColor("Gray3"),
                ThemeColorStyle.Gray4 => GetThemeColor("Gray4"),
                ThemeColorStyle.Gray5 => GetThemeColor("Gray5"),
                ThemeColorStyle.Gray6 => GetThemeColor("Gray6"),
                ThemeColorStyle.Gray7 => GetThemeColor("Gray7"),
                ThemeColorStyle.Gray8 => GetThemeColor("Gray8"),
                ThemeColorStyle.Gray9 => GetThemeColor("Gray9"),
                ThemeColorStyle.Gray10 => GetThemeColor("Gray10"),
                // Text
                ThemeColorStyle.Text => GetThemeColor("Orc.Colors.Text"),
                _ => throw Log.ErrorAndCreateException(_ => new ArgumentOutOfRangeException(nameof(colorStyle)), string.Empty)
            };
        });
    }

    public SolidColorBrush GetThemeColorBrush(string resourceName)
    {
        return _resourceBrushesCache.GetFromCacheOrFetch(resourceName, () =>
        {
            var color = GetThemeColor(resourceName);
            return color.ToSolidColorBrush();
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
        return _accentColorBrushCache ??= _currentTheme?.PrimaryAccentColor.ToSolidColorBrush()
                                          ?? Application.Current?.TryFindResource("AccentColorBrush") as SolidColorBrush
                                          ?? Brushes.Green;
    }

    private void OnThemeManagerThemeChanged(object? sender, ThemeChangedEventArgs e)
    {
        Log.Debug("Theme has changed, clearing current cache");

        _accentColorBrushCache = null;
        _resourceBrushesCache.Clear();
        _themeColorBrushesCache.Clear();
        _themeColorsCache.Clear();

        _currentTheme = e.NewTheme;
    }

    private void OnAccentColorChanged(object? sender, EventArgs e)
    {
        SynchronizeTheme();
    }

    private void OnBaseColorSchemeChanged(object? sender, EventArgs e)
    {
        SynchronizeTheme();
    }

    public virtual void SynchronizeTheme()
    {
        Log.Debug("Synchronizing theme");

        var themeGenerator = RuntimeThemeGenerator.Current;
        themeGenerator.Options.UseHSL = false;

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
}
