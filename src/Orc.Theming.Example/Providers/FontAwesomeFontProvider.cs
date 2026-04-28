namespace Orc.Theming.Example.Providers;

using System;
using System.Collections.Generic;
using System.Windows.Media;

public class FontAwesomeFontProvider : IFontProvider
{
    public IReadOnlyList<FontInfo> Provide()
    {
        return new[]
        {
            new FontInfo
            {
                Name = "FontAwesome",
                FontFamily = new FontFamily(new Uri("pack://application:,,,/Orc.Theming.Example;component/Resources/Fonts/", UriKind.RelativeOrAbsolute), "./#FontAwesome")
            }
        };
    }
}
