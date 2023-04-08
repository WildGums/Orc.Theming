namespace Orc.Theming.Win32;

using Microsoft.Win32.SafeHandles;
using Win32;

internal class SafeIconHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    public SafeIconHandle()
        : base(true)
    {
    }

    protected override bool ReleaseHandle()
    {
        return User32.DestroyIcon(handle);
    }
}
