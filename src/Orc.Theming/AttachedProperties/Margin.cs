namespace Orc.Theming;

using System.Windows;
using Catel.Windows.Data;

public class Margin : DependencyObject
{
    public static readonly DependencyProperty LeftProperty = DependencyProperty.RegisterAttached(
        "Left", typeof(double), typeof(Margin), new PropertyMetadata(double.NaN, OnMarginDimensionChangedChanged));

    public static double GetLeft(DependencyObject d)
    {
        return (double) d.GetValue(LeftProperty);
    }
    public static void SetLeft(DependencyObject d, double value)
    {
        d.SetValue(LeftProperty, value);
    }

    public static readonly DependencyProperty TopProperty = DependencyProperty.RegisterAttached(
        "Top", typeof(double), typeof(Margin), new PropertyMetadata(double.NaN, OnMarginDimensionChangedChanged));

    public static double GetTop(DependencyObject d)
    {
        return (double) d.GetValue(TopProperty);
    }
    public static void SetTop(DependencyObject d, double value)
    {
        d.SetValue(TopProperty, value);
    }

    public static readonly DependencyProperty RightProperty = DependencyProperty.RegisterAttached(
        "Right", typeof(double), typeof(Margin), new PropertyMetadata(double.NaN, OnMarginDimensionChangedChanged));

    public static double GetRight(DependencyObject d)
    {
        return (double) d.GetValue(RightProperty);
    }
    public static void SetRight(DependencyObject d, double value)
    {
        d.SetValue(RightProperty, value);
    }

    public static readonly DependencyProperty BottomProperty = DependencyProperty.RegisterAttached(
        "Bottom", typeof(double), typeof(Margin), new PropertyMetadata(double.NaN, OnMarginDimensionChangedChanged));

    public static double GetBottom(DependencyObject d)
    {
        return (double) d.GetValue(BottomProperty);
    }
    public static void SetBottom(DependencyObject d, double value)
    {
        d.SetValue(BottomProperty, value);
    }

    private static void OnMarginDimensionChangedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not FrameworkElement frameworkElement)
        {
            return;
        }

        frameworkElement.UnsubscribeFromDependencyProperty(nameof(FrameworkElement.Margin), OnMarginChanged);
        frameworkElement?.SubscribeToDependencyProperty(nameof(FrameworkElement.Margin), OnMarginChanged);
    }

    private static void OnMarginChanged(object? sender, DependencyPropertyValueChangedEventArgs e)
    {
        if(sender is not FrameworkElement frameworkElement)
        {
            return;
        }

        frameworkElement.UnsubscribeFromDependencyProperty(nameof(FrameworkElement.Margin), OnMarginChanged);

        var left = GetLeft(frameworkElement);
        var top = GetTop(frameworkElement);
        var right = GetRight(frameworkElement);
        var bottom = GetBottom(frameworkElement);

        if (e.NewValue is Thickness newValue)
        {
            left = double.IsNaN(left) ? newValue.Left : left;
            top = double.IsNaN(top) ? newValue.Top : top;
            right = double.IsNaN(right) ? newValue.Right : right;
            bottom = double.IsNaN(bottom) ? newValue.Bottom : bottom;
        }

        frameworkElement.Margin = new Thickness(left, top, right, bottom);
    }
}
