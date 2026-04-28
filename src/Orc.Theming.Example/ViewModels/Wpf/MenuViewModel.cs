namespace Orc.Theming.Example.ViewModels;

using System;
using Catel.MVVM;

public class MenuViewModel : ViewModelBase
{
    public MenuViewModel(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }

    public override string Title { get { return "Menu"; } }
}
