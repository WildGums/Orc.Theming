namespace Orc.Theming.Coloring
{
    using System.Windows.Media;

    /// <summary>
    /// The integer colors.
    /// </summary>
    public static class IntColors
    {
        /// <summary>
        /// The color white
        /// </summary>
        public static readonly int White = -1;

        /// <summary>
        /// The black color.
        /// </summary>
        public static readonly int Black = -16777216;

        /// <summary>
        /// The dark gray.
        /// </summary>
        public static readonly int DarkGray = -5658199;

        /// <summary>
        /// The light gray.
        /// </summary>
        public static readonly int LightGray = -2631721;

        /// <summary>
        /// The medium gray.
        /// </summary>
        public static readonly int MediumGray = -4079167;

        /// <summary>
        /// The soft gray.
        /// </summary>
        public static readonly int SoftGray = -1052689;

        /// <summary>
        /// The red.
        /// </summary>
        public static readonly int Red = -65536;

        /// <summary>
        /// The blue.
        /// </summary>
        public static readonly int Blue = -16776961;

        /// <summary>
        /// The Gold colour.
        /// </summary>
        public static readonly int Gold = ColorFromRgb(0xff, 0xd7, 0).ToInt();

        /// <summary>
        /// The Yellow colour.
        /// </summary>
        public static readonly int Yellow = ColorFromRgb(0xff, 0xff, 0).ToInt();

        /// <summary>
        /// The transparent soft gray color
        /// </summary>
        public static readonly int TransparentSoftGray = 520093696;

        /// <summary>
        /// Creates a color from RGB values with alpha = 1.
        /// </summary>
        /// <param name="r">R channel.</param>
        /// <param name="g">G channel.</param>
        /// <param name="b">B channel.</param>
        /// <returns>New color.</returns>
        private static Color ColorFromRgb(byte r, byte g, byte b)
        {
            return Color.FromRgb(r, g, b);
        }
    }
}
