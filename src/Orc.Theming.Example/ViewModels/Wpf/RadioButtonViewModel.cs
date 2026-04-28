namespace Orc.Theming.Example.ViewModels;

using Catel.MVVM;
using System;

public class RadioButtonViewModel : ViewModelBase
{
    public RadioButtonViewModel(IServiceProvider serviceProvider) 
        : base(serviceProvider)
    {
    }

    public override string Title { get { return "RadioButton"; } }
}
