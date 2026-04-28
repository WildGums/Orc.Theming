namespace Orc.Theming.Example.ViewModels;

using Catel.MVVM;
using System;

public class RepeatButtonViewModel : ViewModelBase
{
    public RepeatButtonViewModel(IServiceProvider serviceProvider) 
        : base(serviceProvider)
    {
    }

    public override string Title { get { return "Repeat button"; } }
}
