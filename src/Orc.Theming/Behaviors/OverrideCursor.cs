namespace Orc.Theming;

using System.Windows;
using System.Windows.Input;
using Catel.Windows.Interactivity;

public class OverrideCursor : BehaviorBase<FrameworkElement>
{
    public static readonly DependencyProperty CursorProperty = DependencyProperty.Register(
        nameof(Cursor), typeof(Cursor), typeof(OverrideCursor), new PropertyMetadata(Cursors.Arrow));

    private OverrideCursorScope? _currentScope;

    public Cursor Cursor
    {
        get => (Cursor)GetValue(CursorProperty);
        set => SetValue(CursorProperty, value);
    }

    protected override void OnAssociatedObjectLoaded()
    {
        var frameworkElement = AssociatedObject;

        frameworkElement.MouseEnter += OnMouseEnter;
        frameworkElement.MouseLeave += OnMouseLeave;
    }

    protected override void OnAssociatedObjectUnloaded()
    {
        var frameworkElement = AssociatedObject;

        frameworkElement.MouseEnter -= OnMouseEnter;
        frameworkElement.MouseLeave -= OnMouseLeave;
    }

    private void OnMouseEnter(object sender, MouseEventArgs e)
    {
        _currentScope = new OverrideCursorScope(Cursor);
    }

    private void OnMouseLeave(object sender, MouseEventArgs e)
    {
        _currentScope?.Dispose();
    }
}
