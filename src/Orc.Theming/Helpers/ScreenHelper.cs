namespace Orc.Theming;

using System;
using System.Drawing;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using Size = System.Windows.Size;

public static class ScreenHelper
{
    private static Size DpiCache;

    public static Size GetDpi()
    {
        if (DpiCache.Width != 0d && DpiCache.Height != 0d)
        {
            return DpiCache;
        }

        var dpiXProperty = typeof(SystemParameters).GetProperty("DpiX", BindingFlags.NonPublic | BindingFlags.Static);
        var dpiYProperty = typeof(SystemParameters).GetProperty("Dpi", BindingFlags.NonPublic | BindingFlags.Static);

        DpiCache.Width = dpiXProperty?.GetValue(null, null) as int? ?? 96;
        DpiCache.Height = dpiYProperty?.GetValue(null, null) as int? ?? 96;

        return DpiCache;
    }

    public static Rectangle GetScreenBounds(Window window)
    {
        ArgumentNullException.ThrowIfNull(window);

        var windowInteropHelper = new WindowInteropHelper(window);
        var screen = Screen.FromHandle(windowInteropHelper.Handle);

        return screen.Bounds;
    }
}
