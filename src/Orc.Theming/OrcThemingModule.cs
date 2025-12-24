namespace Orc.Theming
{
    using Catel.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Orc.Theming.Coloring;

    /// <summary>
    /// Core module which allows the registration of default services in the service collection.
    /// </summary>
    public static class OrcThemingModule
    {
        public static IServiceCollection AddOrcThemingServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<IAccentColorService, AccentColorService>();
            serviceCollection.TryAddSingleton<IBaseColorSchemeService, BaseColorSchemeService>();
            serviceCollection.TryAddSingleton<IFontSizeService, FontSizeService>();
            serviceCollection.TryAddSingleton<IResourceDictionaryService, ResourceDictionaryService>();
            serviceCollection.TryAddSingleton<IThemeService, ThemeService>();
            serviceCollection.TryAddSingleton<IColorGenerator, ColorGenerator>();

            var themeManager = ControlzEx.Theming.ThemeManager.Current;
            themeManager.RegisterLibraryThemeProvider(new LibraryThemeProvider());
            serviceCollection.AddSingleton(themeManager);
            serviceCollection.AddSingleton<ThemeManager>();

            serviceCollection.AddSingleton<ILanguageSource>(new LanguageResourceSource("Orc.Theming", "Orc.Theming.Properties", "Resources"));

            return serviceCollection;
        }
    }
}
