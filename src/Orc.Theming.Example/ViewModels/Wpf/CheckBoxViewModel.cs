namespace Orc.Theming.Example.ViewModels;

using System;
using Catel.MVVM;

public class CheckBoxViewModel : ViewModelBase
{
    public CheckBoxViewModel(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }

    public override string Title { get { return "CheckBox"; } }
}
