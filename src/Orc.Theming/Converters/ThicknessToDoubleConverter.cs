namespace Orc.Theming.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using Catel.MVVM.Converters;

    internal class ThicknessToDoubleConverter : ValueConverterBase
    {
        protected override object Convert(object value, Type targetType, object parameter)
        {
            if (value is Thickness borderThickness)
            {
                return borderThickness.Left;
            }

            return 1;
        }
    }
}
