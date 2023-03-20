namespace Orc.Theming.Example.Views;

using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Documents;

public partial class RichTextBoxView
{
    public RichTextBoxView()
    {
        InitializeComponent();
    }

    private void OnSpellCheckCheckBoxChecked(object sender, System.Windows.RoutedEventArgs e)
    {
        ExampleRichTextBox.SpellCheck.IsEnabled = (sender as CheckBox)?.IsChecked ?? false;
    }

    private void OnHyperlinkMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var hyperlink = (Hyperlink)sender;
        var ps = new ProcessStartInfo(hyperlink.NavigateUri.ToString())
        {
            UseShellExecute = true,
            Verb = "open"
        };
#pragma warning disable IDISP004 // Don't ignore created IDisposable
        Process.Start(ps);
#pragma warning restore IDISP004 // Don't ignore created IDisposable
    }
}