namespace Orc.Theming
{
    using System.Windows.Media;

    public class ThemeInfo
    {
        public ThemeInfo()
        {
            BaseColorScheme = string.Empty;
        }

        public string BaseColorScheme { get; set; }

        public Color AccentBaseColor { get; set; }

        public Color HighlightColor { get; set; }
    }
}
