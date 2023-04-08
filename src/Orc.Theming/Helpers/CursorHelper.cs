namespace Orc.Theming;

using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Win32;
using Point = System.Windows.Point;
using Size = System.Windows.Size;

//Idea was taken from: 
//https://wpf.2000things.com/2012/12/17/713-setting-the-cursor-to-an-image-of-an-uielement-while-dragging/
public static class CursorHelper
{
    public static Cursor CreateCursor(Bitmap bmp)
    {
        var iconInfo = new IconInfo();
        User32.GetIconInfo(bmp.GetHicon(), ref iconInfo);

        iconInfo.XHotspot = 0;
        iconInfo.YHotspot = 0;
        iconInfo.FIcon = false;

        var cursorHandle = User32.CreateIconIndirect(ref iconInfo);
        return CursorInteropHelper.Create(cursorHandle);
    }

    public static Cursor CreateCursor(UIElement element)
    {
        element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        element.Arrange(new Rect(default, element.DesiredSize));

        var desiredSize = element.DesiredSize;
        if (desiredSize.Width <= 0 || desiredSize.Height <= 0)
        {
            throw new ArgumentException("Can't create Cursor from Visual with zero size");
        }

        var dpi = ScreenHelper.GetDpi();
        var rtb = new RenderTargetBitmap((int)element.DesiredSize.Width, (int)element.DesiredSize.Height, dpi.Width, dpi.Height, PixelFormats.Pbgra32);

        rtb.Render(element);

        var encoder = new PngBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(rtb));

        using var ms = new MemoryStream();
        encoder.Save(ms);
        using var bmp = new Bitmap(ms);
        return CreateCursor(bmp);
    }
}
