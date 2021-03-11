namespace Orc.Theming
{
    using System;
    using System.Collections.Generic;

    public interface IBaseColorSchemeService
    {
        event EventHandler<EventArgs> BaseColorSchemeChanged;

        /// <summary>
        /// returns the available base colors (at least one)
        /// </summary>
        /// <returns>at least one base color, otherwise GetBaseColor will throw an exception</returns>
        IReadOnlyList<string> GetAvailableBaseColorSchemes();

        /// <summary>
        /// returns the available base colors image uris (at least one)
        /// </summary>
        /// <returns>at least one base color image uris, otherwise GetBaseColor will throw an exception</returns>
        IReadOnlyList<KeyValuePair<string, string>> GetAvailableBaseColorSchemeUris();

        string GetBaseColorScheme();
        bool SetBaseColorScheme(string scheme);
    }
}
