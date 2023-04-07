namespace Orc.Theming;

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32.SafeHandles;

//Idea is taken from: 
//https://wpf.2000things.com/2012/12/17/713-setting-the-cursor-to-an-image-of-an-uielement-while-dragging/
public class CursorHelper
{
    private static class NativeMethods
    {
        public struct IconInfo
        {
            public bool FIcon;
            public int XHotspot;
            public int YHotspot;
            public IntPtr HbmMask;
            public IntPtr HbmColor;
        }

        [DllImport("user32.dll")]
        public static extern SafeIconHandle CreateIconIndirect(ref IconInfo icon);

        [DllImport("user32.dll")]
        public static extern bool DestroyIcon(IntPtr hIcon);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);
    }

    internal class SafeIconHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        public SafeIconHandle()
            : base(true)
        {
        }

        protected override bool ReleaseHandle()
        {
            return NativeMethods.DestroyIcon(handle);
        }
    }

    public static Cursor CreateCursor(System.Drawing.Bitmap bmp)
    {
        var iconInfo = new NativeMethods.IconInfo();
        NativeMethods.GetIconInfo(bmp.GetHicon(), ref iconInfo);

        iconInfo.XHotspot = 0;
        iconInfo.YHotspot = 0;
        iconInfo.FIcon = false;

        var cursorHandle = NativeMethods.CreateIconIndirect(ref iconInfo);
        return CursorInteropHelper.Create(cursorHandle);
    }


    public static Cursor CreateCursor(UIElement element)
    {
        element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        element.Arrange(new Rect(new Point(), element.DesiredSize));

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
        using var bmp = new System.Drawing.Bitmap(ms);
        return CreateCursor(bmp);
    }
}
