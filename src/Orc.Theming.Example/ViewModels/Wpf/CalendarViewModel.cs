namespace Orc.Theming.Example.ViewModels;

using System;
using Catel.MVVM;

public class CalendarViewModel : ViewModelBase
{
    public CalendarViewModel(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }

    public override string Title { get { return "Calendar"; } }
}
