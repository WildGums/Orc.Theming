namespace Orc.Theming
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using Catel.Windows.Interactivity;

    public class Square : BehaviorBase<FrameworkElement>
    {
        private bool _isChanging;

        public Square()
        {

        }

        /// <summary>
        /// Determines whether to use the horizontal or vertical size as primary scale.
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof(Orientation),
            typeof(Orientation), typeof(Square), new PropertyMetadata(Orientation.Vertical, (sender, e) => ((Square)sender).OnOrientationChanged()));

        protected override void OnAssociatedObjectLoaded()
        {
            base.OnAssociatedObjectLoaded();

            AssociatedObject.SizeChanged += OnSizeChanged;

            UpdateSize();
        }

        protected override void OnAssociatedObjectUnloaded()
        {
            AssociatedObject.SizeChanged -= OnSizeChanged;

            base.OnAssociatedObjectUnloaded();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_isChanging)
            {
                return;
            }

            UpdateSize();
        }

        private void OnOrientationChanged()
        {
            UpdateSize();
        }

        private void UpdateSize()
        {
            try
            {
                _isChanging = true;

                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        AssociatedObject.SetCurrentValue(FrameworkElement.HeightProperty, AssociatedObject.ActualWidth);
                        break;

                    case Orientation.Vertical:
                        AssociatedObject.SetCurrentValue(FrameworkElement.WidthProperty, AssociatedObject.ActualHeight);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(Orientation), Orientation, "Unexpected orientation value.");
                }
            }
            finally
            {
                _isChanging = false;
            }
        }
    }
}
