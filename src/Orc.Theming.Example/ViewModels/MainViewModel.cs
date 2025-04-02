namespace Orc.Theming.Example.ViewModels;

using System.Threading.Tasks;
using Catel.Configuration;
using Catel.MVVM;
using Catel.Services;
using Orc.FileSystem;

public class MainViewModel : ViewModelBase
{
    private readonly IFontSizeService _fontSizeService;
    private readonly IConfigurationService _configurationService;
    private readonly IAppDataService _appDataDirectory;
    private readonly IDirectoryService _directoryService;

    public MainViewModel(IFontSizeService fontSizeService, IConfigurationService configurationService,
        IAppDataService appDataDirectory, IDirectoryService directoryService)
    {
        _fontSizeService = fontSizeService;
        _configurationService = configurationService;
        _appDataDirectory = appDataDirectory;
        _directoryService = directoryService;

        DeferValidationUntilFirstSaveCall = false;
    }

    public override string Title => "Orc.Theming example";

    protected override async Task CloseAsync()
    {
        var roamingAppData = _appDataDirectory.GetApplicationDataDirectory(Catel.IO.ApplicationDataTarget.UserRoaming);
        _directoryService.Create(roamingAppData);

        _configurationService.SetRoamingValue("FontSize", _fontSizeService.GetFontSize());

        await _configurationService.SaveAsync(ConfigurationContainer.Roaming);

        await base.CloseAsync();
    }
}
