﻿namespace Orc.Theming.Converters;

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Catel.Logging;

internal class ProgressBarSizeToTrackRadiusConverter : IMultiValueConverter
{
    private const double DefaultRadius = 10;

    private static readonly ILog Log = LogManager.GetCurrentClassLogger();

    public object? Convert(object[]? values, Type targetType, object? parameter, CultureInfo? culture)
    {
        if (values is null)
        {
            return DefaultRadius;
        }

        try
        {
            if (values.Length < 3)
            {
                throw Log.ErrorAndCreateException<InvalidOperationException>("Wrong argument count passed to converter");
            }

            var strokeThickness = (Thickness)values[0];

            if ((values[1] is not double size1 || double.IsNaN(size1)) || 
                (values[2] is not double size2 || double.IsNaN(size2)))
            {
                return DefaultRadius;
            }

            var diameter = Math.Min(size1, size2);

            return (diameter - strokeThickness.Left * 2) / 2;
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            return DefaultRadius;
        }
    }

    public object?[] ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo? culture)
    {
        return new[]
        {
            value
        };
    }
}
