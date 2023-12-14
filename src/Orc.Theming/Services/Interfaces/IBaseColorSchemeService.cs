﻿namespace Orc.Theming;

using System;
using System.Collections.Generic;

public interface IBaseColorSchemeService
{
    event EventHandler<EventArgs>? BaseColorSchemeChanged;

    /// <summary>
    /// returns the available base colors (at least one)
    /// </summary>
    /// <returns>at least one base color, otherwise GetBaseColor will throw an exception</returns>
    IReadOnlyList<string> GetAvailableBaseColorSchemes();

    string GetBaseColorScheme();
    bool SetBaseColorScheme(string scheme);
}