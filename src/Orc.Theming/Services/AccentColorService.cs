namespace Orc.Theming
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using Catel.Logging;

    public class AccentColorService : IAccentColorService
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private Color? _accentColor;

        public event EventHandler<EventArgs>? AccentColorChanged;

        public virtual Color GetAccentColor()
        {
            if (_accentColor.HasValue)
            {
                return _accentColor.Value;
            }

            var accentColorBrush = Application.Current.TryFindResource("AccentColorBrush") as SolidColorBrush;
            var finalBrush = accentColorBrush ?? Brushes.Orange;

            _accentColor = finalBrush.Color;

            return _accentColor.Value;
        }

        public virtual bool SetAccentColor(Color color)
        {
            if ((_accentColor ?? Colors.Transparent) == color)
            {
                return false;
            }

            Log.Info($"Setting accent color '{color}'");

            _accentColor = color;

            RaiseAccentColorChanged();

            return true;
        }

        protected void RaiseAccentColorChanged()
        {
            AccentColorChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
