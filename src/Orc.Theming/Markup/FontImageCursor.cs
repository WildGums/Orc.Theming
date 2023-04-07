namespace Orc.Theming;

using System;
using System.Windows.Controls;
using System.Windows.Media;

public class FontImageCursor : FontImage
{
    public FontImageCursor()
    {
    }

    public FontImageCursor(string itemName)
        : base(itemName)
    {
    }

    public double Width { get; set; } = 20d;
    public double Height { get; set; } = 20d;

    protected override object ProvideDynamicValue(IServiceProvider? serviceProvider)
    {
        var cursorImageSource = base.ProvideDynamicValue(serviceProvider) as ImageSource;
        
        var image = new Image
        {
            Source = cursorImageSource,
            Width = Width,
            Height = Height
        };

#pragma warning disable IDISP005
        return CursorHelper.CreateCursor(image);
#pragma warning restore IDISP005
    }
}
