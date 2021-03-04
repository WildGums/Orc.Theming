namespace Orc.Theming
{
    using System.Globalization;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Media;

    public class AsterixAdorner : Adorner
    {
        private readonly Thickness _padding;
        private readonly PositionCorner _positionCorner;
        private double? _lastDpiUpdate;
        private FormattedText _cachedformattedText;

        public AsterixAdorner(UIElement adornedElement, Thickness padding, PositionCorner positionCorner)
          : base(adornedElement)
        {
            _padding = padding;
            _positionCorner = positionCorner;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
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
            DrawTextOnPosition(drawingContext);
        }
        private void DrawTextOnPosition(DrawingContext drawingContext)
        {
            // Note: drawingContext.DrawGlyphRun can be used for better performance
            var drawRect = new Rect(AdornedElement.DesiredSize);

            switch (_positionCorner)
            {
                case PositionCorner.TopLeft:
                    drawingContext.DrawText(_cachedformattedText, new Point(drawRect.TopLeft.X + _padding.Left, drawRect.TopLeft.Y + _padding.Top));
                    break;
                case PositionCorner.BottomRight:
                    drawingContext.DrawText(_cachedformattedText, new Point(drawRect.BottomRight.X - _padding.Right, drawRect.BottomRight.Y - _padding.Bottom));
                    break;
                case PositionCorner.TopRight:
                    drawingContext.DrawText(_cachedformattedText, new Point(drawRect.TopRight.X - _padding.Right, drawRect.TopRight.Y + _padding.Top));
                    break;
                case PositionCorner.BottomLeft:
                    drawingContext.DrawText(_cachedformattedText, new Point(drawRect.BottomLeft.X + _padding.Left, drawRect.BottomLeft.Y - _padding.Bottom));
                    break;
                default:
                    drawingContext.DrawText(_cachedformattedText, new Point(drawRect.TopRight.X - _padding.Right, drawRect.TopRight.Y + _padding.Top));
                    break;
            }
        }
    }
}
