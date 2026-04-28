namespace Orc.Theming;

using System.Collections.Generic;

public interface IFontProvider
{
    IReadOnlyList<FontInfo> Provide();
}
