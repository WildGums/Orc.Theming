namespace Orc.Theming.Tests.Helpers;

using System;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;
using NUnit.Framework;

[TestFixture]
public class CursorHelperTests
{
    [Test]
    public void CreateCursor_From_Bitmap_Should_Return_Valid_Cursor()
    {
        using var bitmap = new Bitmap(20, 20);

        using var cursor = CursorHelper.CreateCursor(bitmap);
        var safeHandle = GetCursorHandle(cursor);

        Assert.That(safeHandle.IsInvalid, Is.False);
        Assert.That(safeHandle.IsClosed, Is.False);
    }

    [Test]
    [Apartment(ApartmentState.STA)]
    public void CreateCursor_From_Visual_Should_Return_Valid_Cursor()
    {
        var button = new Button
        {
            Width = 10,
            Height = 10,
        };

        using var cursor = CursorHelper.CreateCursor(button);
        var safeHandle = GetCursorHandle(cursor);

        Assert.That(safeHandle.IsInvalid, Is.False);
        Assert.That(safeHandle.IsClosed, Is.False);
    }

    [Test]
    [Apartment(ApartmentState.STA)]
    public void CreateCursor_From_Visual_With_Zero_Size_Should_ThrowArgumentException()
    {
        var button = new Button();

        Assert.Throws<ArgumentException>(() => CursorHelper.CreateCursor(button),
            "Can't create Cursor from Visual with zero size");
    }

    private CursorHelper.SafeIconHandle GetCursorHandle(Cursor cursor)
    {
        var handleProperty = typeof(Cursor).GetProperty("Handle", BindingFlags.Instance |
                                                                  BindingFlags.NonPublic |
                                                                  BindingFlags.Public);

        return handleProperty.GetValue(cursor) as CursorHelper.SafeIconHandle;
    }
}
