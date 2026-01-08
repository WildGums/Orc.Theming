namespace Orc
{
    using System.Collections.Generic;
    using System.Windows;
    using Catel.IoC;
    using Catel.Services;
    using Catel.ThirdPartyNotices;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Orc.Theming;
    using Orc.Theming.Coloring;

    /// <summary>
    /// Core module which allows the registration of default services in the service collection.
    /// </summary>
    public static class OrcThemingModule
    {
        public static IServiceCollection AddOrcTheming(this IServiceCollection serviceCollection)
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

            serviceCollection.AddSingleton<FontImageInitializer>();

            serviceCollection.AddSingleton<ILanguageSource>(new LanguageResourceSource("Orc.Theming", "Orc.Theming.Properties", "Resources"));

            serviceCollection.AddSingleton<IThirdPartyNotice>((x) => new LibraryThirdPartyNotice("Orc.Theming", "https://github.com/wildgums/orc.theming"));

            return serviceCollection;
        }

        private class FontImageInitializer : IInitializeAtStartup
        {
            private readonly IEnumerable<IFontProvider> _fontProviders;

            public FontImageInitializer(IEnumerable<IFontProvider> fontProviders)
            {
                _fontProviders = fontProviders;
            }

            public void Initialize()
            {
                var application = Application.Current;

                foreach (var fontProvider in _fontProviders)
                {
                    var fontInfos = fontProvider.Provide();

                    foreach (var fontInfo in fontInfos)
                    {
                        var fontName = fontInfo.Name;
                        var fontFamily = fontInfo.FontFamily;

                        FontImage.RegisterFont(fontName, fontFamily);

                        if (application is not null)
                        {
                            application.Resources[fontName] = fontFamily;
                        }
                    }
                }
            }
        }
    }
}
