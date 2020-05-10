namespace Orc.Theming.Example.Views
{
    using System.Windows.Controls;

    public partial class TextBoxView
    {
        public TextBoxView()
        {
            InitializeComponent();
        }

        private void TextBox_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            textBox.SelectionStart = 0;
            textBox.SelectionLength = textBox.Text.Length;
        }
    }
}
