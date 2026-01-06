namespace Orc.Theming.Example.ViewModels;

using Catel.MVVM;
using System;
using System.Threading.Tasks;

public class TextBoxViewModel : ViewModelBase
{
    public TextBoxViewModel(IServiceProvider serviceProvider) 
        : base(serviceProvider)
    {
    }

    public override string Title { get { return "TextBox"; } }
}
