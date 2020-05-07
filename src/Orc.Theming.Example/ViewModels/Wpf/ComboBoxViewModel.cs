namespace Orc.Theming.Example.ViewModels
{
    using Catel.MVVM;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Documents;

    public class ComboBoxViewModel : ViewModelBase
    {
        public ComboBoxViewModel(/* dependency injection here */)
        {
            Items = new List<string>(new []
            {
                "Item 1",
                "Item 2",
                "Item 3", 
                "Item 4",
                "Item 5"
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
