namespace Orc.Theming.Converters
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using Catel.MVVM.Converters;

    internal class ProgressBarTrackToIndicatorStartPointConverter : ValueConverterBase
    {
        protected override object? Convert(object? value, Type targetType, object? parameter)
        {
            if (value is EllipseGeometry trackGeometry)
            {
                return new Point(trackGeometry.RadiusX, trackGeometry.RadiusY * -1);
            }

            return Size.Empty;
        }
    }
}
