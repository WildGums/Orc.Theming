namespace Orc.Theming;

using System.Windows.Media;

public static class ColorHelper
{
    public static uint GetAlpha(int color)
    {
        var alpha = (byte)(color >> 24);
        return alpha;
    }

    public static uint GetRed(int color)
    {
        var red = (byte)(color >> 16);
        return red;
    }

    public static uint GetGreen(int color)
    {
        var green = (byte)(color >> 8);
        return green;
    }

    public static uint GetBlue(int color)
    {
        var blue = (byte)color;
        return blue;
    }

    public static Color GetColor(int colorAsInt)
    {
        var alpha = GetAlpha(colorAsInt);
        var red = GetRed(colorAsInt);
        var green = GetGreen(colorAsInt);
        var blue = GetBlue(colorAsInt);

        return Color.FromArgb((byte)alpha, (byte)red, (byte)green, (byte)blue);
    }
}