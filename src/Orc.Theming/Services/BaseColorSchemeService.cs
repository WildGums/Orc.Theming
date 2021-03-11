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

        private string _baseColorScheme;

        public BaseColorSchemeService()
        {
            _themeManager = ControlzEx.Theming.ThemeManager.Current;
        }

        public string GetBaseColorScheme()
        {
            return _baseColorScheme ??= GetAvailableBaseColorSchemes()[0];
        }

        public event EventHandler<EventArgs> BaseColorSchemeChanged;

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

        private IReadOnlyList<string> getDefaultColors()
            => new[] { "Light", "Dark" };

        public virtual IReadOnlyList<string> GetAvailableBaseColorSchemes()
        {
            var baseColors = _themeManager.BaseColors;
            if (baseColors.Count > 0)
            {
                return baseColors;
            }

            return getDefaultColors();
        }

        public virtual IReadOnlyList<KeyValuePair<string, string>> GetAvailableBaseColorSchemeUris()
        {
            IEnumerable<string> baseColors = _themeManager.BaseColors;
            if (baseColors.Count() == 0)
                baseColors = getDefaultColors();

            var baseColorUris = new List<KeyValuePair<string, string>>();

            var dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var imgDir = System.IO.Path.Combine(dir, "Resources", "Images");
            foreach (var baseColor in baseColors)
            {
                var correspondingUri = "";
                foreach (var filename in System.IO.Directory.GetFiles(imgDir, "*.png"))
                {
                    if (filename.ContainsIgnoreCase(baseColor))
                    {
                        correspondingUri = filename;
                        break;
                    }
                }
                baseColorUris.Add(new KeyValuePair<string, string>(baseColor, correspondingUri));
            }

            return baseColorUris;
        }
    }
}
