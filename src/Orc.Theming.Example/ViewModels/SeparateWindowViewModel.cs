namespace Orc.Theming.Example.ViewModels;

using System;
using System.Collections.ObjectModel;
using Catel.MVVM;
using Wizards.ExampleWizard;

public class SeparateWindowViewModel : ViewModelBase
{
    public SeparateWindowViewModel(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        Skills = new ObservableCollection<Skill>(new[]
        {
            new Skill { Name = "C#" },
            new Skill { Name = "Catel" },
            new Skill { Name = "MVVM" },
            new Skill { Name = "WPF" },
        });
    }

    public ObservableCollection<Skill> Skills { get; set; }
}
