namespace Orc.Theming.Example.Views
{
    using System;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Data;
    using Catel.Reflection;

    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            CanCloseUsingEscape = false;

            LoadTabItems();
        }

        private void LoadTabItems()
        {
            TabControl.Items.Add(new TabItem
            {
                Header = "Overview",
                Content = new ControlsView()
            });

            var viewsToAdd = (from viewType in GetType().Assembly.GetTypesEx()
                              where typeof(UserControl).IsAssignableFromEx(viewType) &&
                                    viewType != typeof(ControlsView)
                              orderby viewType.Name
                              select viewType);

            foreach (var viewToAdd in viewsToAdd)
            {
                var instance = (Catel.Windows.Controls.UserControl)Activator.CreateInstance(viewToAdd);

                var tabItem = new TabItem();

                var header = viewToAdd.Name;
                var lastIndex = header.LastIndexOf("View");
                if (lastIndex > 0)
                {
                    header = header.Substring(0, lastIndex);
                }

                tabItem.Header = header;
                tabItem.Content = instance;

                TabControl.Items.Add(tabItem);
            }
        }
    }
}
