namespace Orc.Theming
{
    using System.Windows;
    using System.Windows.Documents;
    using Catel.Logging;
    using Catel.Windows.Interactivity;

    public class MandatoryFieldBehavior : BehaviorBase<FrameworkElement>
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public MandatoryFieldBehavior()
        {
        }

        public static void SetIsMandatory(DependencyObject element, bool value)
        {
            element.SetValue(IsMandatoryProperty, value);
        }

        public static bool GetIsMandatory(DependencyObject element)
        {
            return (bool)element.GetValue(IsMandatoryProperty);
        }

        public static readonly DependencyProperty IsMandatoryProperty =
            DependencyProperty.RegisterAttached("IsMandatory", typeof(bool), typeof(MandatoryFieldBehavior), new PropertyMetadata(false, (sender, e) => OnIsMandatoryChanged(sender, e)));

        private static void OnIsMandatoryChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var associatedObject = (FrameworkElement)sender;
            var myAdornerLayer = AdornerLayer.GetAdornerLayer(associatedObject);

            if (myAdornerLayer is null)
            {
                Log.Warning($"FrameworkElement {associatedObject} doesn't have adorner layer in the visual tree");
            }

            myAdornerLayer.Add(new AsterixAdorner(associatedObject, new Thickness(0, 2, 4, 0)));
        }
    }
}
