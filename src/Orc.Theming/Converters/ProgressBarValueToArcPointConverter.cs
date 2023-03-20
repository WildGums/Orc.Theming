namespace Orc.Theming.Converters;

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Catel.Logging;

internal class ProgressBarValueToArcPointConverter : IMultiValueConverter
{
    private static readonly ILog Log = LogManager.GetCurrentClassLogger();

    public object? Convert(object[]? values, Type targetType, object? parameter, CultureInfo? culture)
    {
        if (values is null)
        {
            return null;
        }

        if (values.Length < 5)
        {
            throw Log.ErrorAndCreateException<InvalidOperationException>("Wrong argument count passed to converter");
        }

        var progressValue = (double)values[0];
        var progressStart = (double)values[1];
        var progressEnd = (double)values[2];

        var radiusX = (double)values[3];
        var radiusY = (double)values[4];

        const int cy = 0;

        var arcStartPoint = new Point(radiusX, -1 * radiusY);

        var angle = progressValue / (progressEnd - progressStart) * 2 * Math.PI;

        var arcX = radiusX + (arcStartPoint.X - radiusX) * Math.Cos(angle) + (cy - arcStartPoint.Y) * Math.Sin(angle);
        var arcY = cy + (arcStartPoint.Y - cy) * Math.Cos(angle) + (arcStartPoint.X - radiusX) * Math.Sin(angle);

        return new Point(arcX, arcY);
    }

    public object?[] ConvertBack(object value, Type[] targetTypes, object? parameter, CultureInfo? culture)
    {
        return new[]
        {
            value
        };
    }
}
