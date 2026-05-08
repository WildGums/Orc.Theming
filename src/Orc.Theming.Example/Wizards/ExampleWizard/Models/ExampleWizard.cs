namespace Orc.Theming.Example.Wizards.ExampleWizard;

using System;
using System.Threading.Tasks;
using Catel.Logging;
using Microsoft.Extensions.Logging;
using Wizard;

public class ExampleWizard : WizardBase
{
    private static readonly ILogger Logger = LogManager.GetLogger(typeof(ExampleWizard));

    public ExampleWizard(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        Title = "Orc.Theming wizard example"; 

        this.AddPage<PersonWizardPage>(serviceProvider);
        this.AddPage<AgeWizardPage>(serviceProvider);
        this.AddPage<SkillsWizardPage>(serviceProvider);
        this.AddPage<ComponentsWizardPage>(serviceProvider);
        this.AddPage<SummaryWizardPage>(serviceProvider);
    }

    public bool ShowInTaskbarWrapper
    {
        get {  return ShowInTaskbar; }
        set { ShowInTaskbar = value; }
    }

    public override Task<bool> ResumeAsync()
    {
        Logger.LogInformation("Resuming wizard");

        return base.ResumeAsync();
    }
}
