namespace Orc.Theming.Example.ViewModels;

using System.Threading.Tasks;
using Catel.MVVM;
using Catel.Services;
using Wizards.ExampleWizard;
using Wizard;
using System;

public class WizardViewModel : ViewModelBase
{
    private readonly IWizardService _wizardService;
    private readonly IUIVisualizerService _uiVisualizerService;

    public WizardViewModel(IWizardService wizardService, IUIVisualizerService uiVisualizerService)
    {
        ArgumentNullException.ThrowIfNull(wizardService);
        ArgumentNullException.ThrowIfNull(uiVisualizerService);

        _wizardService = wizardService;
        _uiVisualizerService = uiVisualizerService;

        ShowWizard = new TaskCommand(OnShowWizardExecuteAsync);
        ShowSeparateWindow = new TaskCommand(OnShowSeparateWindowAsync);
    }
        
    public TaskCommand ShowWizard { get; }
    public TaskCommand ShowSeparateWindow { get; }

    private async Task OnShowWizardExecuteAsync()
    {
        await _wizardService.ShowWizardAsync<ExampleWizard>();
    }

    private async Task OnShowSeparateWindowAsync()
    {
        await _uiVisualizerService.ShowDialogAsync<SeparateWindowViewModel>();
    }
}
