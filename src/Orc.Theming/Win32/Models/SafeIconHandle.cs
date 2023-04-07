namespace Orc.Theming;

using Microsoft.Win32.SafeHandles;
using Win32;

public class SafeIconHandle : SafeHandleZeroOrMinusOneIsInvalid
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
