namespace Orc.Theming;

using System;
using System.Collections.Generic;
using System.Linq;
using Catel;
using Catel.Logging;
using Microsoft.Extensions.Logging;

public class BaseColorSchemeService : IBaseColorSchemeService
{
    private readonly ILogger<BaseColorSchemeService> _logger;
    private readonly ControlzEx.Theming.ThemeManager _themeManager;

    private string _baseColorScheme = "Light";

    public BaseColorSchemeService(ILogger<BaseColorSchemeService> logger)
    {
        _logger = logger;
        _themeManager = ControlzEx.Theming.ThemeManager.Current;
    }

    public event EventHandler<EventArgs>? BaseColorSchemeChanged;

    public string GetBaseColorScheme()
    {
        return _baseColorScheme;
    }

    public bool SetBaseColorScheme(string scheme)
    {
        if (_baseColorScheme.EqualsIgnoreCase(scheme) || !GetAvailableBaseColorSchemes().Contains(scheme))
        {
            return false;
        }

        _logger.LogInformation($"Setting base color scheme '{scheme}'");

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
