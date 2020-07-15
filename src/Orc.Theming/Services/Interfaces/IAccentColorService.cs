namespace Orc.Theming
{
    using System;
    using System.Windows.Media;

    public interface IAccentColorService
    {
        Color GetAccentColor();

        bool SetAccentColor(Color color);

        event EventHandler<EventArgs> AccentColorChanged;
    }
}
