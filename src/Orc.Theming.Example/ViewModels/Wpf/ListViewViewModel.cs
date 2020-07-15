namespace Orc.Theming.Example.ViewModels
{
    using Catel.MVVM;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ListViewViewModel : ViewModelBase
    {
        public ListViewViewModel(/* dependency injection here */)
        {
            Items = new List<string>(new[]
            {
                "Item 1",
                "Item 2",
                "Item 3",
                "Item 4",
                "Item 5",
                "Item 6",
                "Item 7",
                "Item 8",
                "Item 9",
                "Item 10",
                "Item 11",
                "Item 12",
                "Item 13",
                "Item 14",
                "Item 15",
                "Item 16",
                "Item 17",
                "Item 18",
                "Item 19",
                "Item 20",
                "Item 21",
                "Item 22",
                "Item 23fsdfsdafasdfsadfsdfsadfsdafasfsadfdsafas",
                "Item 24",
                "Item 25",
            });

            SelectedItem = Items.FirstOrDefault();
        }

        public List<string> Items { get; private set; }

        public string SelectedItem { get; set; }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            // TODO: subscribe to events here
        }

        protected override async Task CloseAsync()
        {
            // TODO: unsubscribe from events here

            await base.CloseAsync();
        }
    }
}
