namespace Orc.Theming
{
    using System;
    using Catel.Logging;
    using Catel.Windows.Markup;

    public class ThemeColor : UpdatableMarkupExtension
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly ControlzEx.Theming.ThemeManager _controlzThemeManager;
        private readonly ThemeManager _themeManager;

        public ThemeColor()
        {
            AllowUpdatableStyleSetters = true;

            _controlzThemeManager = ControlzEx.Theming.ThemeManager.Current;
            _themeManager = ThemeManager.Current;
        }

        public ThemeColor(ThemeColorStyle themeColorStyle)
            : this()
        {
            ThemeColorStyle = themeColorStyle;
        }

        public ThemeColorStyle ThemeColorStyle { get; set; }

        public string ResourceName { get; set; }

        protected override void OnTargetObjectLoaded()
        {
            base.OnTargetObjectLoaded();

            _controlzThemeManager.ThemeChanged += OnThemeChanged;
        }

        protected override void OnTargetObjectUnloaded()
        {
            _controlzThemeManager.ThemeChanged -= OnThemeChanged;

            base.OnTargetObjectUnloaded();
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            UpdateValue();
        }

        protected override object ProvideDynamicValue(IServiceProvider serviceProvider)
        {
            var resourceName = ResourceName;
            if (!string.IsNullOrWhiteSpace(resourceName))
            {
                return _themeManager.GetThemeColor(resourceName);
            }

            return _themeManager.GetThemeColor(ThemeColorStyle);
        }
    }
}
