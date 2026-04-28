namespace Orc.Theming.Example.ViewModels;

using Catel.MVVM;
using System;
using System.Threading.Tasks;

public class TabControlViewModel : ViewModelBase
{
    public TabControlViewModel(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }

    public override string Title { get { return "TabControl"; } }
}
