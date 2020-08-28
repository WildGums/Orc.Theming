namespace Orc.Theming.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;
    using Catel.Logging;

    /// <summary>
    /// Converts current progress double value to boolean ArcSegment.IsLargeArc
    /// </summary>
    internal class ProgressBarValueToArcTypeConverter : IMultiValueConverter
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Count() != 3)
            {
                Log.ErrorAndCreateException<InvalidOperationException>("Wrong argument count passed to converter");
            }

            var progressValue = (double)values[0];
            var progressStart = (double)values[1];
            var progressEnd = (double)values[2];

            var angle = progressValue / (progressEnd - progressStart) * 360;

            return angle > 180;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[] { value };
        }
    }
}
