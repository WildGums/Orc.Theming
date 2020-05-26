namespace Orc.Theming
{
    using System.Windows.Media;

    public static class ColorExtensions
    {
        public static SolidColorBrush ToSolidColorBrush(this Color color, double opacity = 1d)
        {
            var brush = new SolidColorBrush(color)
            {
                Opacity = opacity
            };

            brush.Freeze();

            return brush;
        }

        public static Color ConvertToNonAlphaColor(Color backgroundColor, Color accentColor)
        {
            var alphaNormalized = accentColor.A / (double)255;

            // calculate rgb from argb with same color
            var newColorR = (byte)(accentColor.R * alphaNormalized + backgroundColor.R * (1 - alphaNormalized));
            byte newColorG = (byte)(accentColor.G * alphaNormalized + backgroundColor.G * (1 - alphaNormalized));
            var newColorB = (byte)(accentColor.B * alphaNormalized + backgroundColor.B * (1 - alphaNormalized));

            return Color.FromRgb(newColorR, newColorG, newColorB);
        }
    }
}
