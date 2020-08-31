namespace Orc.Theming.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using Catel.Logging;
    using Catel.MVVM.Converters;

    internal class ProgressBarTrackRadiusToTrackOffsetConverter : IMultiValueConverter
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Count() < 2)
            {
                throw Log.ErrorAndCreateException<InvalidOperationException>("Wrong argument count passed to converter");
            }

            var trackPath = values[0] as double?;
            var trackGeometry = values[1] as EllipseGeometry;

            if (trackPath is null || trackGeometry is null)
            {
                throw Log.ErrorAndCreateException<InvalidOperationException>("Wrong argument type");
            }

            var propertyPath = parameter.ToString();

            switch (propertyPath)
            {
                case nameof(EllipseGeometry.RadiusX):

                    return trackGeometry.RadiusX + trackPath;

                case nameof(EllipseGeometry.RadiusY):
                    return trackGeometry.RadiusY + trackPath;

                default:
                    return DependencyProperty.UnsetValue;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[] { value };
        }
    }
}
