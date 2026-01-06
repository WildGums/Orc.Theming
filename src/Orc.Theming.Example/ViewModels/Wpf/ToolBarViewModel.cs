namespace Orc.Theming.Example.ViewModels;

using Catel.MVVM;
using System;

public class ToolBarViewModel : ViewModelBase
{
    public ToolBarViewModel(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }

    public override string Title { get { return "ToolBar"; } }
}
