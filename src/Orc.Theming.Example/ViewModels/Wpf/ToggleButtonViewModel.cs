namespace Orc.Theming.Example.ViewModels;

using Catel.MVVM;
using System;

public class ToggleButtonViewModel : ViewModelBase
{
    public ToggleButtonViewModel(IServiceProvider serviceProvider) 
        : base(serviceProvider)
    {
    }

    public override string Title { get { return "Toggle button"; } }
}
