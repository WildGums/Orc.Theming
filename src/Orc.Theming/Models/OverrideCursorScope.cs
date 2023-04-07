namespace Orc.Theming;

using System;
using System.Collections.Generic;
using System.Windows.Input;

public sealed class OverrideCursorScope : IDisposable
{
    private static readonly Stack<Cursor> Cursors = new();

    public OverrideCursorScope(Cursor changeToCursor)
    {
        Cursors.Push(changeToCursor);

        if (Mouse.OverrideCursor != changeToCursor)
        {
            Mouse.OverrideCursor = changeToCursor;
        }
    }

    public void Dispose()
    {
        
        var cursor = Cursors.Count > 0 ? Cursors.Pop() : null;

        Mouse.OverrideCursor = cursor;
    }
}
