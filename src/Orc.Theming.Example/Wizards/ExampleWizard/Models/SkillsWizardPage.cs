namespace Orc.Theming.Example.Wizards.ExampleWizard;

using System.Collections.ObjectModel;
using System.Text;
using Wizard;

public class SkillsWizardPage : WizardPageBase
{
    public SkillsWizardPage()
    {
        Title = "Skills";
        Description = "Select the skills";

        Skills = new ObservableCollection<Skill>(new[]
        {
            new Skill { Name = "C#" },
            new Skill { Name = "Catel" },
            new Skill { Name = "MVVM" },
            new Skill { Name = "WPF" },
        });
    }

    public ObservableCollection<Skill> Skills { get; }

    public override ISummaryItem GetSummary()
    {
        var summary = new StringBuilder();

        foreach (var skill in Skills)
        {
            if (skill.IsSelected)
            {
                summary.AppendLine(skill.Name);
            }
        }

        return new SummaryItem
        {
            Title = "Skills",
            Summary = summary.ToString()
        };
    }
}
