namespace Orc.Theming
{
    using System.Collections.Generic;
    using System.Globalization;
    using ControlzEx.Theming;

    public class LibraryThemeProvider : ControlzEx.Theming.LibraryThemeProvider
    {
        public LibraryThemeProvider()
            : base(true)
        {
        }

        public override void FillColorSchemeValues(Dictionary<string, string> values, RuntimeThemeColorValues colorValues)
        {
            // Orc / Orchestra colors
            values.Add("Orc.Colors.AccentBaseColor", colorValues.AccentBaseColor.ToString(CultureInfo.InvariantCulture));
            values.Add("Orc.Colors.AccentColor", colorValues.AccentBaseColor.ToString(CultureInfo.InvariantCulture));
            values.Add("Orc.Colors.AccentColor1", colorValues.AccentColor80.ToString(CultureInfo.InvariantCulture));
            values.Add("Orc.Colors.AccentColor2", colorValues.AccentColor60.ToString(CultureInfo.InvariantCulture));
            values.Add("Orc.Colors.AccentColor3", colorValues.AccentColor40.ToString(CultureInfo.InvariantCulture));
            values.Add("Orc.Colors.AccentColor4", colorValues.AccentColor20.ToString(CultureInfo.InvariantCulture));
            values.Add("Orc.Colors.AccentColor5", colorValues.AccentColor20.ToString(CultureInfo.InvariantCulture));

            values.Add("Orc.Colors.AccentColor80", colorValues.AccentColor80.ToString(CultureInfo.InvariantCulture));
            values.Add("Orc.Colors.AccentColor60", colorValues.AccentColor60.ToString(CultureInfo.InvariantCulture));
            values.Add("Orc.Colors.AccentColor40", colorValues.AccentColor40.ToString(CultureInfo.InvariantCulture));
            values.Add("Orc.Colors.AccentColor20", colorValues.AccentColor20.ToString(CultureInfo.InvariantCulture));

            values.Add("Orc.Colors.HighlightColor", colorValues.HighlightColor.ToString(CultureInfo.InvariantCulture));
            values.Add("Orc.Colors.IdealForegroundColor", colorValues.IdealForegroundColor.ToString(CultureInfo.InvariantCulture));

            // ControlzEx colors
            values.Add("ControlzEx.Colors.AccentBaseColor", colorValues.AccentBaseColor.ToString(CultureInfo.InvariantCulture));
            values.Add("ControlzEx.Colors.AccentColor80", colorValues.AccentColor80.ToString(CultureInfo.InvariantCulture));
            values.Add("ControlzEx.Colors.AccentColor60", colorValues.AccentColor60.ToString(CultureInfo.InvariantCulture));
            values.Add("ControlzEx.Colors.AccentColor40", colorValues.AccentColor40.ToString(CultureInfo.InvariantCulture));
            values.Add("ControlzEx.Colors.AccentColor20", colorValues.AccentColor20.ToString(CultureInfo.InvariantCulture));

            values.Add("ControlzEx.Colors.HighlightColor", colorValues.HighlightColor.ToString(CultureInfo.InvariantCulture));
            values.Add("ControlzEx.Colors.IdealForegroundColor", colorValues.IdealForegroundColor.ToString(CultureInfo.InvariantCulture));
        }
    }
}
