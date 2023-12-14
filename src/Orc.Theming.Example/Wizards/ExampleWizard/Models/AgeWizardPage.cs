namespace Orc.Theming.Example.Wizards.ExampleWizard;

using Wizard;

public class AgeWizardPage : WizardPageBase
{
    public AgeWizardPage()
    {
        Title = "Age";
        Description = "Specify the age of the person";
        IsOptional = true;
    }

    public string Age { get; set; }
}
