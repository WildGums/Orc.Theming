namespace Orc.Theming.Example.ViewModels;

using Catel.MVVM;
using System;
using System.Threading.Tasks;

public class ThemeImageViewModel : ViewModelBase
{
    public ThemeImageViewModel(IServiceProvider serviceProvider) 
        : base(serviceProvider)
    {
    }

    public override string Title { get { return "Theme image"; } }
}
