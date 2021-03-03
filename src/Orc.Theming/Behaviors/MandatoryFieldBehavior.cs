namespace Orc.Theming
{
    using System.Windows;
    using System.Windows.Documents;
    using Catel.Windows.Interactivity;

    public class MandatoryFieldBehavior : BehaviorBase<FrameworkElement>
    {
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
            myAdornerLayer.Add(new AsterixAdorner(associatedObject, new Thickness(0, 2, 4, 0)));
        }
    }
}
