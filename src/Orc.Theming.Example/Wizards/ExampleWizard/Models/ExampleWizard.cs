namespace Orc.Theming.Example.Wizards.ExampleWizard
{
    using System.Threading.Tasks;
    using Catel.IoC;
    using Catel.Logging;
    using Orc.Wizard;

    public class ExampleWizard : WizardBase
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public ExampleWizard(ITypeFactory typeFactory)
            : base(typeFactory)
        {
            Title = "Orc.Theming wizard example"; 

            this.AddPage<PersonWizardPage>();
            this.AddPage<AgeWizardPage>();
            this.AddPage<SkillsWizardPage>();
            this.AddPage<ComponentsWizardPage>();
            this.AddPage<SummaryWizardPage>();
        }

        public bool ShowInTaskbarWrapper
        {
            get {  return ShowInTaskbar; }
            set { ShowInTaskbar = value; }
        }

        public override async Task ResumeAsync()
        {
            Log.Info("Resuming wizard");

            await base.ResumeAsync();
        }
    }
}
