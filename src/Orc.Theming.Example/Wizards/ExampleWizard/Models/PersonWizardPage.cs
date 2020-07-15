namespace Orc.Theming.Example.Wizards.ExampleWizard
{
    using Orc.Wizard;

    public class PersonWizardPage : WizardPageBase
    {
        public PersonWizardPage()
        {
            Title = "Person";
            Description = "Enter the details of the person";
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public override ISummaryItem GetSummary()
        {
            return new SummaryItem
            {
                Title = "Person",
                Summary = $"{FirstName} {LastName}"
            };
        }
    }
}
