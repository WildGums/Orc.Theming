namespace Orc.Theming
{
    using System;

    public interface IFontSizeService
    {
        event EventHandler<EventArgs>? FontSizeChanged;

        double GetFontSize();

        bool SetFontSize(double fontSize);
    }
}
