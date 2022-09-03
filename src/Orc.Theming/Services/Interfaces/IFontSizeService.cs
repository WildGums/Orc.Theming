namespace Orc.Theming
{
    using System;
    using System.Windows.Media;

    public interface IFontSizeService
    {
        double GetFontSize();

        bool SetFontSize(double fontSize);

        event EventHandler<EventArgs> FontSizeChanged;
    }
}
