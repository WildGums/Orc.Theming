using Catel.IoC;
using Catel.Services;
using Orc.Theming;
using Orc.Theming.Coloring;

/// <summary>
/// Used by the ModuleInit. All code inside the Initialize method is ran as soon as the assembly is loaded.
/// </summary>
public static class ModuleInitializer
{
    /// <summary>
    /// Initializes the module.
    /// </summary>
    public static void Initialize()
    {
        var serviceLocator = ServiceLocator.Default;

        serviceLocator.RegisterTypeIfNotYetRegistered<IAccentColorService, AccentColorService>();
        serviceLocator.RegisterTypeIfNotYetRegistered<IBaseColorSchemeService, BaseColorSchemeService>();
        serviceLocator.RegisterTypeIfNotYetRegistered<IFontSizeService, FontSizeService>();
        serviceLocator.RegisterTypeIfNotYetRegistered<IResourceDictionaryService, ResourceDictionaryService>();
        serviceLocator.RegisterTypeIfNotYetRegistered<IThemeService, ThemeService>();
        serviceLocator.RegisterTypeIfNotYetRegistered<IColorGenerator, ColorGenerator>();

        var themeManager = ControlzEx.Theming.ThemeManager.Current;
        themeManager.RegisterLibraryThemeProvider(new LibraryThemeProvider());
        serviceLocator.RegisterInstance(themeManager);
        serviceLocator.RegisterType<ThemeManager>();

        var languageService = serviceLocator.ResolveRequiredType<ILanguageService>();
        languageService.RegisterLanguageSource(new LanguageResourceSource("Orc.Theming", "Orc.Theming.Properties", "Resources"));
    }
}
