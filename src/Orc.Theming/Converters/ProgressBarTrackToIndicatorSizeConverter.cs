namespace Orc.Theming.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;
    using Catel.MVVM.Converters;

    internal class ProgressBarTrackToIndicatorSizeConverter : ValueConverterBase
    {
        protected override object Convert(object value, Type targetType, object parameter)
        {
            if (value is EllipseGeometry trackGeometry)
            {
                return new Size(trackGeometry.RadiusX, trackGeometry.RadiusY);
            }

            return Size.Empty;
        }
    }
}
