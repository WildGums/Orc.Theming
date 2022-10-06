namespace Orc.Theming.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Media;
    using Catel.Logging;

    internal class ProgressBarTrackRadiusToTrackOffsetConverter : IMultiValueConverter
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
        {
            if (values is null)
            {
                return null;
            }

            if (values.Length < 2)
            {
                throw Log.ErrorAndCreateException<InvalidOperationException>("Wrong argument count passed to converter");
            }

            if (!(values[0] is double trackPath) || !(values[1] is EllipseGeometry trackGeometry))
            {
                throw Log.ErrorAndCreateException<InvalidOperationException>("Wrong argument type");
            }

            var propertyPath = parameter?.ToString();

            return propertyPath switch
            {
                nameof(EllipseGeometry.RadiusX) => trackGeometry.RadiusX + (double?)trackPath,
                nameof(EllipseGeometry.RadiusY) => trackGeometry.RadiusY + (double?)trackPath,
                _ => DependencyProperty.UnsetValue
            };
        }

        public object?[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo? culture)
        {
            return new[] {value};
        }
    }
}
