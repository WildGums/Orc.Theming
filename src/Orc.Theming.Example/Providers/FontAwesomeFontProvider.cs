namespace Orc.Theming.Example.Providers
{
    using System;
    using System.Windows.Media;

    public class FontAwesomeFontProvider : IFontProvider
    {
        public FontInfo Provide()
        {
            return new FontInfo
            {
                Name = "FontAwesome",
                FontFamily = new FontFamily(new Uri("pack://application:,,,/Orc.Theming.Example;component/Resources/Fonts/", UriKind.RelativeOrAbsolute), "./#FontAwesome")
            };
        }
    }
}
