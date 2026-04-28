namespace Orc.Theming.Example.ViewModels;

using System;
using System.Threading.Tasks;
using Catel.MVVM;

public class ButtonViewModel : ViewModelBase
{
    public ButtonViewModel(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }

    public override string Title { get { return "Button"; } }
}
