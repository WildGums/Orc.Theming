namespace Orc.Theming.Behaviors;

using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using Catel.Windows;
using Catel.Windows.Interactivity;
using Window = System.Windows.Window;

public class CorrectPopupWidth : BehaviorBase<Popup>
{
    private FrameworkElement? _parentElement;

    public FrameworkElement? ParentElement
    {
        get => (FrameworkElement?)GetValue(ParentElementProperty);
        set => SetValue(ParentElementProperty, value);
    }

    public static readonly DependencyProperty ParentElementProperty = DependencyProperty.Register(
        nameof(ParentElement), typeof(FrameworkElement), typeof(CorrectPopupWidth), new PropertyMetadata(default(FrameworkElement),
            (sender, args) => ((CorrectPopupWidth)sender).OnParentElementChanged(args)));

    private void OnParentElementChanged(DependencyPropertyChangedEventArgs args)
    {
        _parentElement = args.NewValue as FrameworkElement;
    }

    protected override void OnAssociatedObjectLoaded()
    {
        AssociatedObject.Opened += OnOpened;
    }

    protected override void OnAssociatedObjectUnloaded()
    {
        AssociatedObject.Opened -= OnOpened;
    }

    private void OnOpened(object? sender, EventArgs e)
    {
        if (_parentElement is null)
        {
            return;
        }

        var window = AssociatedObject.FindLogicalOrVisualAncestorByType<Window>();
        if (window is null)
        {
            return;
        }

        var screenBounds = ScreenHelper.GetScreenBounds(window);
        var currentScreenWidth = screenBounds.Width + screenBounds.X;
        var rightCornerPointOnScreen = _parentElement.PointToScreen(new Point(_parentElement.ActualWidth, 0)).X;

        var dpi = ScreenHelper.GetDpi().Width / 96d;

        var width = _parentElement.ActualWidth;
        if (currentScreenWidth > rightCornerPointOnScreen)
        {
            var leftCornerPointOnScreen = _parentElement.PointToScreen(new Point(0, 0)).X;
            if (leftCornerPointOnScreen < screenBounds.X)
            {
                width = (rightCornerPointOnScreen - screenBounds.X) / dpi;
            }
        }
        else
        {
            width = (_parentElement.ActualWidth * dpi - (rightCornerPointOnScreen - currentScreenWidth)) / dpi;
        }

        if (width < 0)
        {
            width = _parentElement.ActualWidth;
        }

        AssociatedObject.SetCurrentValue(FrameworkElement.WidthProperty, width);
    }
}