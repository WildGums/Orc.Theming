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

    internal class ProgressBarValueToArcPointConverter : IMultiValueConverter
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Count() < 5)
            {
                throw Log.ErrorAndCreateException<InvalidOperationException>("Wrong argument count passed to converter");
            }

            var progressValue = (double)values[0];
            var progressStart = (double)values[1];
            var progressEnd = (double)values[2];

            var radiusX = (double)values[3];
            var radiusY = (double)values[4];

            var cx = radiusX;
            var cy = 0;

            var arcStartPoint = new Point(cx, -1 * radiusY);

            var angle = progressValue / (progressEnd - progressStart) * 2 * Math.PI;

            var arcX = cx + (arcStartPoint.X - cx) * Math.Cos(angle) + (cy - arcStartPoint.Y) * Math.Sin(angle);
            var arcY = cy + (arcStartPoint.Y - cy) * Math.Cos(angle) + (arcStartPoint.X - cx) * Math.Sin(angle);

            return new Point(arcX, arcY);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[] { value };
        }
    }
}
