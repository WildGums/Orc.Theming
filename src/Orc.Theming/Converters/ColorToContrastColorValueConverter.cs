namespace Orc.Theming.Converters
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using Catel.MVVM.Converters;

    public class ColorToOpaqueColorConverter : ValueConverterBase
    {
        protected override object Convert(object value, Type targetType, object parameter)
        {
            if (value is Color color)
            {
                return color.RemoveAlpha();
            }

            if (value is SolidColorBrush solidColorBrush)
            {
                return new SolidColorBrush(solidColorBrush.Color.RemoveAlpha());
            }

            return DependencyProperty.UnsetValue;
        }
    }


    public class ColorToContrastColorValueConverter : ValueConverterBase
    {
        protected override object Convert(object value, Type targetType, object parameter)
        {
            if (value is Color color)
            {
                return color.FindContrast();
            }

            if (value is SolidColorBrush solidColorBrush)
            {
                return new SolidColorBrush(solidColorBrush.Color.FindContrast());
            }

            return DependencyProperty.UnsetValue;
        }
    }
}
