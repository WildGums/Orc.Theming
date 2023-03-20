namespace Orc.Theming;

using System;
using System.Windows.Media;

public interface IAccentColorService
{
    event EventHandler<EventArgs>? AccentColorChanged;

    Color GetAccentColor();

    bool SetAccentColor(Color color);
}