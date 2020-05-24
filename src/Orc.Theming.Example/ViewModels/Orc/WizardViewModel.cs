namespace Orc.Theming.Example.ViewModels
{
    using System.Threading.Tasks;
    using Catel;
    using Catel.MVVM;
    using Orc.Theming.Example.Wizards.ExampleWizard;
    using Orc.Wizard;

    public class WizardViewModel : ViewModelBase
    {
        private readonly IWizardService _wizardService;

        public WizardViewModel(IWizardService wizardService)
        {
            Argument.IsNotNull(() => wizardService);

            _wizardService = wizardService;

            ShowWizard = new TaskCommand(OnShowWizardExecuteAsync);
        }

        public TaskCommand ShowWizard { get; private set; }

        private async Task OnShowWizardExecuteAsync()
        {
            await _wizardService.ShowWizardAsync<ExampleWizard>();
        }
    }
}
