namespace Orc.Theming.Example.ViewModels;

using System;
using Catel.MVVM;

public class ThemeTemplateViewModel : ViewModelBase
{
    public ThemeTemplateViewModel(IServiceProvider serviceProvider) 
        : base(serviceProvider)
    {
    }

    public override string Title => "Theme";
}
