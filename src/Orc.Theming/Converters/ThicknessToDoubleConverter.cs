namespace Orc.Theming.Converters;

using System;
using System.Windows;
using Catel.MVVM.Converters;

internal class ThicknessToDoubleConverter : ValueConverterBase
{
    protected override object Convert(object? value, Type targetType, object? parameter)
    {
        if (value is Thickness borderThickness)
        {
            return borderThickness.Left;
        }

        return 1;
    }
}
