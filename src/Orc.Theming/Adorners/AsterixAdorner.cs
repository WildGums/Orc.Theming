namespace Orc.Theming
{
    using System.Globalization;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Media;

    public class AsterixAdorner : Adorner
    {
        private double? _lastDpiUpdate;
        private FormattedText _cachedformattedText;

        public AsterixAdorner(UIElement adornedElement)
          : base(adornedElement)
        {
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var drawRect = new Rect(AdornedElement.DesiredSize);

            var currentDpi = VisualTreeHelper.GetDpi(this).PixelsPerDip;

            if (_cachedformattedText is null ||
                currentDpi != _lastDpiUpdate)
            {
                _lastDpiUpdate = currentDpi;

                _cachedformattedText = new FormattedText
                   (
                      "*",
                      CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                      new Typeface(new FontFamily("Arial"), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal, new FontFamily("Global Monospace")),
                      18, Brushes.Red, currentDpi
                   );
            }

            // Note: drawingContext.DrawGlyphRun can be used for better performance
            drawingContext.DrawText(_cachedformattedText, drawRect.TopRight);
        }
    }
}
