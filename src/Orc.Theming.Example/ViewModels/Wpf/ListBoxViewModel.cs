namespace Orc.Theming.Example.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using Catel.MVVM;

public class ListBoxViewModel : ViewModelBase
{
    public ListBoxViewModel(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        Items = new List<string>(new[]
        {
            "Item 1",
            "Item 2",
            "Item 3",
            "Item 4",
            "Item 5"
        });

        SelectedItem = Items.FirstOrDefault();
    }

    public List<string> Items { get; }

    public string SelectedItem { get; set; }
}
