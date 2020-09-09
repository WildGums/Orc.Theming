namespace Orc.Theming.Example.ViewModels
{
    using System.Threading.Tasks;
    using Catel;
    using Catel.MVVM;
    using Catel.Services;
    using Wizards.ExampleWizard;
    using Wizard;

    public class WizardViewModel : ViewModelBase
    {
        private readonly IWizardService _wizardService;
        private readonly IUIVisualizerService _uiVisualizerService;

        public WizardViewModel(IWizardService wizardService, IUIVisualizerService uiVisualizerService)
        {
            Argument.IsNotNull(() => wizardService);
            Argument.IsNotNull(() => uiVisualizerService);

            _wizardService = wizardService;
            _uiVisualizerService = uiVisualizerService;

            ShowWizard = new TaskCommand(OnShowWizardExecuteAsync);
            ShowSeparateWindow = new TaskCommand(OnShowSeparateWindow);
        }
        
        public TaskCommand ShowWizard { get; private set; }
        public TaskCommand ShowSeparateWindow { get; private set; }

        private async Task OnShowWizardExecuteAsync()
        {
            await _wizardService.ShowWizardAsync<ExampleWizard>();
        }

        private async Task OnShowSeparateWindow()
        {
            await _uiVisualizerService.ShowDialogAsync<SeparateWindowViewModel>();
        }
    }
}
