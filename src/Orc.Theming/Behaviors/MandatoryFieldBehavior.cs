namespace Orc.Theming
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Documents;
    using Catel.Logging;
    using Catel.Windows.Interactivity;

    public class MandatoryFieldBehavior : BehaviorBase<FrameworkElement>
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public static void SetOffset(DependencyObject element, Thickness value)
        {
            element.SetValue(OffsetProperty, value);
        }

        public static Thickness GetOffset(DependencyObject element)
        {
            return (Thickness)element.GetValue(OffsetProperty);
        }

        public static readonly DependencyProperty OffsetProperty =
            DependencyProperty.RegisterAttached("Offset", typeof(Thickness), typeof(MandatoryFieldBehavior), new PropertyMetadata(new Thickness(0, 0, 0, 0), (sender, e) => DrawAdorner((FrameworkElement)sender)));

        public static void SetPosition(DependencyObject element, PositionCorner value)
        {
            element.SetValue(PositionProperty, value);
        }

        public static PositionCorner GetPosition(DependencyObject element)
        {
            return (PositionCorner)element.GetValue(PositionProperty);
        }

        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.RegisterAttached("Position", typeof(PositionCorner), typeof(MandatoryFieldBehavior), new PropertyMetadata(PositionCorner.TopRight, (sender, e) => DrawAdorner((FrameworkElement)sender)));

        public static void SetIsMandatory(DependencyObject element, bool value)
        {
            element.SetValue(IsMandatoryProperty, value);
        }

        public static bool GetIsMandatory(DependencyObject element)
        {
            return (bool)element.GetValue(IsMandatoryProperty);
        }

        public static readonly DependencyProperty IsMandatoryProperty =
            DependencyProperty.RegisterAttached("IsMandatory", typeof(bool), typeof(MandatoryFieldBehavior), new PropertyMetadata(false, (sender, e) => DrawAdorner((FrameworkElement)sender)));

        private static void DrawAdorner(FrameworkElement element)
        {
            try
            {
                var myAdornerLayer = AdornerLayer.GetAdornerLayer(element);

                if (myAdornerLayer is null)
                {
                    Log.Warning($"FrameworkElement {element} doesn't have adorner layer in the visual tree");
                    return;
                }

                var offset = GetOffset(element);
                var isVisible = GetIsMandatory(element);
                var positionCorner = GetPosition(element);

                var toRemove = myAdornerLayer.GetAdorners(element)?.OfType<AsterixAdorner>() ?? Enumerable.Empty<AsterixAdorner>();

                foreach (var adorner in toRemove)
                {
                    myAdornerLayer.Remove(adorner);
                }

                if (isVisible)
                {
                    myAdornerLayer.Add(new AsterixAdorner(element, offset, positionCorner));
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }
    }
}
