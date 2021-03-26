namespace Orc.Theming
{
    using System;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using Catel.Windows;
    using Catel.Windows.Interactivity;
    using Window = System.Windows.Window;

    public class CorrectPopupWidthBehavior : BehaviorBase<Popup>
    {
        #region Fields
        private FrameworkElement _parentElement;
        #endregion

        private void OnParentElementChanged(DependencyPropertyChangedEventArgs args)
        {
            _parentElement = args.NewValue as FrameworkElement;
        }

        protected override void OnAssociatedObjectLoaded()
        {
            AssociatedObject.Opened += OnOpened;
        }

        protected override void OnAssociatedObjectUnloaded()
        {
            AssociatedObject.Opened -= OnOpened;
        }

        private void OnOpened(object sender, EventArgs e)
        {
            if (_parentElement is null)
            {
                return;
            }

            var window = AssociatedObject.FindLogicalOrVisualAncestorByType<Window>();
            var screenBounds = ScreenHelper.GetScreenBounds(window);
            var currentScreenWidth = screenBounds.Width + screenBounds.X;
            var rightCornerPointOnScreen = _parentElement.PointToScreen(new Point(_parentElement.ActualWidth, 0)).X;

            var dpi = ScreenHelper.GetDpi().Width / 96d;

            var width = _parentElement.ActualWidth;
            if (currentScreenWidth > rightCornerPointOnScreen)
            {
                var leftCornerPointOnScreen = _parentElement.PointToScreen(new Point(0, 0)).X;
                if (leftCornerPointOnScreen < screenBounds.X)
                {
                    width = (rightCornerPointOnScreen - screenBounds.X) / dpi;
                }
            }
            else
            {
                width = (_parentElement.ActualWidth * dpi - (rightCornerPointOnScreen - currentScreenWidth)) / dpi;
            }

            if (width < 0)
            {
                width = _parentElement.ActualWidth;
            }

            AssociatedObject.SetCurrentValue(FrameworkElement.WidthProperty, width);
        }

        #region Dependency properties
        public FrameworkElement ParentElement
        {
            get => (FrameworkElement)GetValue(ParentElementProperty);
            set => SetValue(ParentElementProperty, value);
        }

        public static readonly DependencyProperty ParentElementProperty = DependencyProperty.Register(
            nameof(ParentElement), typeof(FrameworkElement), typeof(CorrectPopupWidthBehavior), new PropertyMetadata(default(FrameworkElement),
                (sender, args) => ((CorrectPopupWidthBehavior)sender).OnParentElementChanged(args)));
        #endregion
    }
}
