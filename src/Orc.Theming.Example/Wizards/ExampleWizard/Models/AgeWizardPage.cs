namespace Orc.Theming.Example.Wizards.ExampleWizard
{
    using Orc.Wizard;

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
}
