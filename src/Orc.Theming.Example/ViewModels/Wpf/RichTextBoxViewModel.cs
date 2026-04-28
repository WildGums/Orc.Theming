namespace Orc.Theming.Example.ViewModels;

using Catel.MVVM;
using System;
using System.Threading.Tasks;

public class RichTextBoxViewModel : ViewModelBase
{
    public RichTextBoxViewModel(IServiceProvider serviceProvider) 
        : base(serviceProvider)
    {
    }

    public override string Title { get { return "RichTextBox"; } }
}
