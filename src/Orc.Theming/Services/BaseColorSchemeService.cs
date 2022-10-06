namespace Orc.Theming
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Catel;
    using Catel.Logging;

    public class BaseColorSchemeService : IBaseColorSchemeService
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly ControlzEx.Theming.ThemeManager _themeManager;

        private string _baseColorScheme = "Light";

        public BaseColorSchemeService()
        {
            _themeManager = ControlzEx.Theming.ThemeManager.Current;
        }

        public event EventHandler<EventArgs>? BaseColorSchemeChanged;

        public string GetBaseColorScheme()
        {
            return _baseColorScheme ??= GetAvailableBaseColorSchemes()[0];
        }

        public bool SetBaseColorScheme(string scheme)
        {
            if (_baseColorScheme.EqualsIgnoreCase(scheme) || !GetAvailableBaseColorSchemes().Contains(scheme))
            {
                return false;
            }

            Log.Info($"Setting base color scheme '{scheme}'");

            _baseColorScheme = scheme;

            BaseColorSchemeChanged?.Invoke(this, EventArgs.Empty);

            return true;
        }

        public virtual IReadOnlyList<string> GetAvailableBaseColorSchemes()
        {
            var baseColors = _themeManager.BaseColors;
            if (baseColors.Count > 0)
            {
                return baseColors;
            }

            return new[] { "Light", "Dark" };
        }
    }
}
