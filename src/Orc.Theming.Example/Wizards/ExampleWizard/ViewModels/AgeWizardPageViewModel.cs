namespace Orc.Theming.Example.Wizards.ExampleWizard.ViewModels;

using System;
using Catel.MVVM;
using Wizard;

public class AgeWizardPageViewModel : WizardPageViewModelBase<AgeWizardPage>
{
    public AgeWizardPageViewModel(AgeWizardPage wizardPage, IServiceProvider serviceProvider)
        : base(wizardPage, serviceProvider)
    {
    }

    [ViewModelToModel]
    public string Age { get; set; }
}
