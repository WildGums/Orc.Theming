namespace Orc.Theming.Win32;

using System;
using System.Runtime.InteropServices;

public static class User32
{
    [DllImport("user32.dll")]
    public static extern SafeIconHandle CreateIconIndirect(ref IconInfo icon);

    [DllImport("user32.dll")]
    public static extern bool DestroyIcon(IntPtr hIcon);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);
}
