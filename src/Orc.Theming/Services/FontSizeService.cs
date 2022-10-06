namespace Orc.Theming
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using Catel.Logging;

    public class FontSizeService : IFontSizeService
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private double? _fontSize;

        public event EventHandler<EventArgs>? FontSizeChanged;

        public virtual double GetFontSize()
        {
            if (!_fontSize.HasValue)
            {
                var mainWindow = Application.Current.MainWindow;
                if (mainWindow is null)
                {
                    // default
                    return 12d;
                }

                _fontSize = TextBlock.GetFontSize(mainWindow);
            }

            return _fontSize.Value;
        }

        public virtual bool SetFontSize(double fontSize)
        {
            Log.Info($"Setting font size '{fontSize}'");

            _fontSize = fontSize;

            var mainWindow = Application.Current.MainWindow;
            if (mainWindow is not null)
            {
                TextBlock.SetFontSize(mainWindow, fontSize);
            }

            RaiseFontSizeChanged();

            return true;
        }

        protected void RaiseFontSizeChanged()
        {
            FontSizeChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
