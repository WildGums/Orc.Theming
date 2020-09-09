namespace Orc.Theming.Example.Views
{
    using System.Windows;
    using System.Windows.Input;

    public sealed partial class ControlsView
    {
        public ControlsView()
        {
            InitializeComponent();
        }

        private void btnActiveGradient_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var pt = Mouse.GetPosition(btnActiveGradient);
            gradRadial.SetCurrentValue(System.Windows.Media.RadialGradientBrush.GradientOriginProperty, 
                new Point(pt.X / btnActiveGradient.Width, pt.Y / btnActiveGradient.Height));
            gradRadial.SetCurrentValue(System.Windows.Media.RadialGradientBrush.CenterProperty, gradRadial.GradientOrigin);
        }

        private void btnActiveGradient_MouseLeave(object sender, MouseEventArgs e)
        {
            gradRadial.SetCurrentValue(System.Windows.Media.RadialGradientBrush.GradientOriginProperty, new Point(0.5, 0.5));   // Default
            gradRadial.SetCurrentValue(System.Windows.Media.RadialGradientBrush.CenterProperty, gradRadial.GradientOrigin);
        }
    }
}
