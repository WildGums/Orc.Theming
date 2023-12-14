namespace Orc.Theming.Example.Wizards.ExampleWizard.ViewModels;

using Catel.MVVM;
using Wizard;

public class AgeWizardPageViewModel : WizardPageViewModelBase<AgeWizardPage>
{
    public AgeWizardPageViewModel(AgeWizardPage wizardPage)
        : base(wizardPage)
    {
    }

    [ViewModelToModel]
    public string Age { get; set; }
}
