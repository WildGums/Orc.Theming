namespace Orc.Theming.Example.ViewModels;

using System;
using System.Threading.Tasks;
using Catel.MVVM;

public class PasswordBoxViewModel : ViewModelBase
{
    public PasswordBoxViewModel(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }

    public override string Title { get { return "PasswordBox"; } }
}
