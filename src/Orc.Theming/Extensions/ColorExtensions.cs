namespace Orc.Theming
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Media;

    public static class ColorExtensions
    {
        private static readonly Color[] ContrastPalette =
        {
            Colors.Black,
            Colors.White
        };

        public static SolidColorBrush ToSolidColorBrush(this Color color, double opacity = 1d)
        {
            var brush = new SolidColorBrush(color)
            {
                Opacity = opacity
            };

            brush.Freeze();

            return brush;
        }

        /// <summary>Set brightness</summary>
        /// <param name="color"></param>
        /// <param name="brightness"></param>
        /// <returns></returns>
        public static Color SetBrightness(this Color color, float brightness)
        {
            var r = Clamp((double)color.R * brightness);
            var g = Clamp((double)color.G * brightness);
            var b = Clamp((double)color.B * brightness);
            return Color.FromArgb(color.A, r, g, b);
        }

        public static Color FindContrast(this Color color)
        {
            var luminance = color.GetPerceptiveLuminance();
            var maxDelta = 0f;
            var bestContrast = color;
            foreach (var contrastColor in ContrastPalette)
            {
                var contrastLuminance = contrastColor.GetPerceptiveLuminance();
                var delta = Math.Abs(luminance - contrastLuminance);
                if (!(delta > maxDelta))
                {
                    continue;
                }

                maxDelta = delta;
                bestContrast = contrastColor;
            }

            return bestContrast;
        }

        public static Color RemoveAlpha(this Color foreground)
        {
            if (foreground.A == 255)
            {
                return foreground;
            }

            var alpha = foreground.A;
            var relativeAlpha = alpha / 255.0;
            var diff = 255 - alpha;
            return Color.FromArgb(255,
                (byte)(foreground.R * relativeAlpha + diff),
                (byte)(foreground.G * relativeAlpha + diff),
                (byte)(foreground.B * relativeAlpha + diff));
        }

        public static float GetPerceptiveLuminance(this Color color)
        {
            return 1 - (0.255f * color.R + 0.655f * color.G + 0.09f * color.B) / 255;
        }

        public static Color[] TransformPalette(this IReadOnlyList<Color> palette, int count)
        {
            var result = new Color[count];

            var step = (palette.Count - 1) / (float)count;
            var i = 0;
            while (i < count)
            {
                var realIndex = step * (i + 1);
                var minIndex = (int)Math.Truncate(realIndex);
                var maxIndex = (int)Math.Ceiling(Math.Round(realIndex, 4));

                if (minIndex == maxIndex)
                {
                    result[i] = palette[minIndex];
                }
                else
                {
                    result[i] = palette[minIndex].InterpolateToColor(realIndex - minIndex, palette[maxIndex]);
                }

                i++;
            }

            return result;
        }

        public static Color InterpolateToColor(this Color color, float mix, Color destColor)
        {
            var alt = 1.0f - mix;

            double x0 = color.R;
            double y0 = color.G;
            double z0 = color.B;

            double x1 = destColor.R;
            double y1 = destColor.G;
            double z1 = destColor.B;

            var mag0 = Math.Sqrt(x0 * x0 + y0 * y0 + z0 * z0);
            var mag1 = Math.Sqrt(x1 * x1 + y1 * y1 + z1 * z1);

            var x = mix * x0 + alt * x1;
            var y = mix * y0 + alt * y1;
            var z = mix * z0 + alt * z1;

            var mag = mix * mag0 + alt * mag1;
            var scale = mag / Math.Sqrt(x * x + y * y + z * z);

            return Color.FromArgb(255,
                Clamp(x * scale),
                Clamp(y * scale),
                Clamp(z * scale));
        }

        private static byte Clamp(double value)
        {
            var x = (int)Math.Round(value);

            if (x > 255)
            {
                return 255;
            }

            if (x < 0)
            {
                return 0;
            }

            return (byte)x;
        }
    }
}
