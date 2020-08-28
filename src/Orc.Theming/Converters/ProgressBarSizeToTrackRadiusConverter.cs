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
    using Catel.Logging;

    internal class ProgressBarSizeToTrackRadiusConverter : IMultiValueConverter
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        private const double DefaultRadius = 10;
        private const double DefaultStrokeThickness = 5;

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (values.Count() != 3)
                {
                    Log.ErrorAndCreateException<InvalidOperationException>("Wrong argument count passed to converter");
                }

                var strokeThickness = (Thickness)values[0];
                var size1 = values[1] as double?;
                var size2 = values[2] as double?;

                if ((!size1.HasValue || Double.IsNaN(size1.Value)) && (!size2.HasValue || Double.IsNaN(size2.Value)))
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
