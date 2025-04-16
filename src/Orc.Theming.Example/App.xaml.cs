namespace Orc.Theming.Example;

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using Catel.Configuration;
using Catel.IoC;
using Catel.Logging;
using Catel.Services;
using Orchestra;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    private static readonly ILog Log = LogManager.GetCurrentClassLogger();

    protected override async void OnStartup(StartupEventArgs e)
    {
#if DEBUG
        LogManager.AddDebugListener();
#endif

        var languageService = ServiceLocator.Default.ResolveRequiredType<ILanguageService>();

        // Note: it's best to use .CurrentUICulture in actual apps since it will use the preferred language
        // of the user. But in order to demo multilingual features for devs (who mostly have en-US as .CurrentUICulture),
        // we use .CurrentCulture for the sake of the demo
        languageService.PreferredCulture = CultureInfo.CurrentCulture;
        languageService.FallbackCulture = new CultureInfo("en-US");

        FontImage.RegisterFont("FontAwesome", new FontFamily(new Uri("pack://application:,,,/Orc.Theming.Example;component/Resources/Fonts/", UriKind.RelativeOrAbsolute), "./#FontAwesome"));
        FontImage.DefaultFontFamily = "FontAwesome";

        this.ApplyTheme();

        var configurationService = ServiceLocator.Default.ResolveType<IConfigurationService>();
        await configurationService.LoadAsync();

        Log.Info("Starting application");
        Log.Info("This log message should show up as debug");

        base.OnStartup(e);

        // Note: run after window has been created
        var fontSize = configurationService.GetRoamingValue("FontSize", 12d);

        var fontSizeService = ServiceLocator.Default.ResolveRequiredType<IFontSizeService>();
        fontSizeService.SetFontSize(fontSize);
    }
}
