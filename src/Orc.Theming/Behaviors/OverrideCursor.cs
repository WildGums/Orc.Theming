namespace Orc.Theming;

using System.Windows;
using System.Windows.Input;
using Catel.Windows.Interactivity;

public class OverrideCursor : BehaviorBase<FrameworkElement>
{
    private Cursor? _previousCursor;

    public Cursor Cursor
    {
        get => (Cursor)GetValue(CursorProperty);
        set => SetValue(CursorProperty, value);
    }

    public static readonly DependencyProperty CursorProperty = DependencyProperty.Register(
        nameof(Cursor), typeof(Cursor), typeof(OverrideCursor), new PropertyMetadata(Cursors.Arrow));

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
        _previousCursor = Mouse.OverrideCursor;
        Mouse.OverrideCursor = Cursor;
    }

    private void OnMouseLeave(object sender, MouseEventArgs e)
    {
        Mouse.OverrideCursor = _previousCursor;
        _previousCursor = null;
    }
}
