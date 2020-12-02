namespace Orc.Theming.Converters
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Data;
    using Catel.Logging;

    internal class ProgressBarSizeToTrackRadiusConverter : IMultiValueConverter
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        private const double DefaultRadius = 10;

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (values.Count() < 3)
                {
                    throw Log.ErrorAndCreateException<InvalidOperationException>("Wrong argument count passed to converter");
                }

                var strokeThickness = (Thickness)values[0];
                var size1 = values[1] as double?;
                var size2 = values[2] as double?;

                if ((!size1.HasValue || double.IsNaN(size1.Value)) && (!size2.HasValue || double.IsNaN(size2.Value)))
                {
                    return DefaultRadius;
                }

                var diameter = Math.Min(size1.Value, size2.Value);

                return (diameter - strokeThickness.Left * 2) / 2;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return DefaultRadius;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[] { value };
        }
    }
}
