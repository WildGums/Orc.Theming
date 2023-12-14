namespace Orc.Theming.Converters;

using System;
using System.Windows;
using System.Windows.Media;
using Catel.MVVM.Converters;

public class ColorToOpaqueColorConverter : ValueConverterBase
{
    protected override object? Convert(object? value, Type targetType, object? parameter)
    {
        return value switch
        {
            Color color => color.RemoveAlpha(),
            SolidColorBrush solidColorBrush => new SolidColorBrush(solidColorBrush.Color.RemoveAlpha()),
            _ => DependencyProperty.UnsetValue
        };
    }
}