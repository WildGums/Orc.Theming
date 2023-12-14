namespace Orc.Theming;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Catel;
using Catel.Logging;
using MethodTimer;

/// <summary>
/// Helper class for WPF styles and themes.
/// </summary>
public static class StyleHelper
{
    /// <summary>
    /// The log.
    /// </summary>
    private static readonly ILog Log = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Gets or sets a value indicating whether style forwarding is enabled. Style forwarding can be
    /// enabled by calling one of the <see cref="CreateStyleForwardersForDefaultStyles(string)"/> overloads.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is style forwarding enabled; otherwise, <c>false</c>.
    /// </value>
    public static bool IsStyleForwardingEnabled { get; private set; }

    /// <summary>
    /// Prefix of a default style key.
    /// </summary>
    private const string DefaultKeyPrefix = "Default";

    /// <summary>
    /// Postfix of a default style key.
    /// </summary>
    private const string DefaultKeyPostfix = "Style";

    /// <summary>
    /// Ensures that an application instance exists and the styles are applied to the application. This method is extremely useful
    /// to apply when WPF is hosted (for example, when loaded as plugin of a non-WPF application).
    /// </summary>
    /// <param name="applicationResourceDictionary">The application resource dictionary.</param>
    /// <param name="defaultPrefix">The default prefix, uses to determine the styles as base for other styles.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="applicationResourceDictionary"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">The <paramref name="defaultPrefix"/> is <c>null</c> or whitespace.</exception>
    [Time]
    public static void EnsureApplicationResourcesAndCreateStyleForwarders(Uri applicationResourceDictionary, string defaultPrefix = DefaultKeyPrefix)
    {
        ArgumentNullException.ThrowIfNull(applicationResourceDictionary);
        Argument.IsNotNullOrWhitespace(nameof(defaultPrefix), defaultPrefix);

        if (Application.Current is not null)
        {
            return;
        }

        try
        {
            // Ensure we have an application
            _ = new Application();

            var app = Application.Current;
            if (app is null)
            {
                return;
            }

            if (Application.LoadComponent(applicationResourceDictionary) is ResourceDictionary resourceDictionary)
            {
                app.Resources.MergedDictionaries.Add(resourceDictionary);
            }

            CreateStyleForwardersForDefaultStyles(app.Resources, defaultPrefix);

            // Create an invisible dummy window to make sure that this is the main window
            var dummyMainWindow = new Window { Visibility = Visibility.Hidden };
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to ensure application resources");
        }
    }

    /// <summary>
    /// Creates style forwarders for default styles. This means that all styles found in the theme that are
    /// name like Default[CONTROLNAME]Style (i.e. "DefaultButtonStyle") will be used as default style for the
    /// control.
    /// This method will use the current application to retrieve the resources. The forwarders will be written to the same dictionary.
    /// </summary>
    /// <param name="defaultPrefix">The default prefix, uses to determine the styles as base for other styles.</param>
    /// <exception cref="ArgumentException">The <paramref name="defaultPrefix"/> is <c>null</c> or whitespace.</exception>
    [Time("{defaultPrefix}")]
    public static void CreateStyleForwardersForDefaultStyles(string defaultPrefix = DefaultKeyPrefix)
    {
        CreateStyleForwardersForDefaultStyles(Application.Current.Resources, defaultPrefix);
    }

    /// <summary>
    /// Creates style forwarders for default styles. This means that all styles found in the theme that are
    /// name like Default[CONTROLNAME]Style (i.e. "DefaultButtonStyle") will be used as default style for the
    /// control.
    /// This method will use the passed resources, but the forwarders will be written to the same dictionary as
    /// the source dictionary.
    /// </summary>
    /// <param name="sourceResources">Resource dictionary to read the keys from (thus that contains the default styles).</param>
    /// <param name="defaultPrefix">The default prefix, uses to determine the styles as base for other styles.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="sourceResources"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">The <paramref name="defaultPrefix"/> is <c>null</c> or whitespace.</exception>
    public static void CreateStyleForwardersForDefaultStyles(ResourceDictionary sourceResources, string defaultPrefix = DefaultKeyPrefix)
    {
        CreateStyleForwardersForDefaultStyles(sourceResources, sourceResources, defaultPrefix);
    }

    /// <summary>
    /// Creates style forwarders for default styles. This means that all styles found in the theme that are
    /// name like Default[CONTROLNAME]Style (i.e. "DefaultButtonStyle") will be used as default style for the
    /// control.
    /// <para/>
    /// This method will use the passed resources.
    /// </summary>
    /// <param name="sourceResources">Resource dictionary to read the keys from (thus that contains the default styles).</param>
    /// <param name="targetResources">Resource dictionary where the forwarders will be written to.</param>
    /// <param name="defaultPrefix">The default prefix, uses to determine the styles as base for other styles.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="sourceResources"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentNullException">The <paramref name="targetResources"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">The <paramref name="defaultPrefix"/> is <c>null</c> or whitespace.</exception>
    public static void CreateStyleForwardersForDefaultStyles(ResourceDictionary sourceResources, ResourceDictionary targetResources, string defaultPrefix = DefaultKeyPrefix)
    {
        CreateStyleForwardersForDefaultStyles(sourceResources, sourceResources, targetResources, defaultPrefix);
    }

    /// <summary>
    /// Creates style forwarders for default styles. This means that all styles found in the theme that are
    /// name like Default[CONTROLNAME]Style (i.e. "DefaultButtonStyle") will be used as default style for the
    /// control.
    /// This method will use the passed resources.
    /// </summary>
    /// <param name="rootResourceDictionary">The root resource dictionary.</param>
    /// <param name="sourceResources">Resource dictionary to read the keys from (thus that contains the default styles).</param>
    /// <param name="targetResources">Resource dictionary where the forwarders will be written to.</param>
    /// <param name="defaultPrefix">The default prefix, uses to determine the styles as base for other styles.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="rootResourceDictionary" /> is <c>null</c>.</exception>
    /// <exception cref="ArgumentNullException">The <paramref name="sourceResources" /> is <c>null</c>.</exception>
    /// <exception cref="ArgumentNullException">The <paramref name="targetResources" /> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">The <paramref name="defaultPrefix" /> is <c>null</c> or whitespace.</exception>
    [Time]
    public static void CreateStyleForwardersForDefaultStyles(ResourceDictionary rootResourceDictionary, ResourceDictionary sourceResources,
        ResourceDictionary targetResources, string defaultPrefix = DefaultKeyPrefix)
    {
        ArgumentNullException.ThrowIfNull(rootResourceDictionary);
        ArgumentNullException.ThrowIfNull(sourceResources);
        ArgumentNullException.ThrowIfNull(targetResources);
        Argument.IsNotNullOrWhitespace(nameof(defaultPrefix), defaultPrefix);

        var foundDefaultStyles = FindDefaultStyles(sourceResources, defaultPrefix);

        // Important: invert order and skip duplicates (but based on Type, not on string key)
        var defaultStylesDictionary = new Dictionary<Type, StyleInfo>();

        for (var i = foundDefaultStyles.Count - 1; i >= 0; i--)
        {
            var styleInfo = foundDefaultStyles[i];

            var targetType = styleInfo.TargetType;
            if (targetType is null)
            {
                continue;
            }

            if (defaultStylesDictionary.TryGetValue(targetType, out _))
            {
                continue;
            }

            defaultStylesDictionary[targetType] = styleInfo;
        }

        // Important note: Styles are coming from Orchestra.Core (implicit) by default. In some cases (such as MahApps, or any other UI lib) 
        // we need to override these styles and ignore them (remove them).
        // 
        // Option A: [Orchestra.Core | Style with {x:Type Button}]                ===\
        //                                                                            ===> [Orchestra.Core | DefaultStyles.xaml with margins] ===> [Final style with key {x:Type Button}]
        // Option B: [Orchestra.Shell.MahApps | Style with "DefaultButtonStyle"]  ===/

        foreach (var styleInfoKeyValuePair in defaultStylesDictionary)
        {
            var defaultStyle = styleInfoKeyValuePair.Value.Style;
            if (defaultStyle is null)
            {
                continue;
            }

            try
            {
                var targetType = defaultStyle.TargetType;
                if (targetType is not null)
                {
                    var style = new Style(targetType, defaultStyle);

                    // Always overwrite
                    targetResources[targetType] = style;
                }
            }
            catch (Exception ex)
            {
                var tag = defaultStyle.TargetType?.ToString() ?? defaultStyle.ToString();
                Log.Warning(ex, "Failed to complete the style for '{0}'", tag);
            }
        }

        IsStyleForwardingEnabled = true;
    }

    [Time]
    private static List<StyleInfo> FindDefaultStyles(ResourceDictionary sourceResources, string defaultPrefix)
    {
        var context = new DefaultStylesContext();

        FindDefaultStyles(context, sourceResources, defaultPrefix);

        return context.Styles;
    }

    private static void FindDefaultStyles(DefaultStylesContext context, ResourceDictionary sourceResources, string defaultPrefix)
    {
        var uri = sourceResources.Source;
        if (uri is not null)
        {
            var uriName = uri.ToString();
            if (!string.IsNullOrWhiteSpace(uriName))
            {
                if (context.ParsedDictionaries.Contains(uriName))
                {
                    return;
                }

                context.ParsedDictionaries.Add(uriName);
            }
        }

        var keys = (from key in sourceResources.Keys as ICollection<object>
            let stringKey = key as string
            where stringKey != null &&
                  (stringKey).StartsWith(defaultPrefix, StringComparison.Ordinal) &&
                  (stringKey).EndsWith(DefaultKeyPostfix, StringComparison.Ordinal)
            select stringKey).Distinct().ToList();

        foreach (var key in keys)
        {
            try
            {
                if (sourceResources[key] is Style style)
                {
                    context.Styles.Add(new StyleInfo(key)
                    {
                        Style = style,
                        SourceDictionary = sourceResources,
                        TargetType = style.TargetType,
                    });
                }
            }
            catch (Exception ex)
            {
                Log.Warning(ex, $"Failed to add a default style ('{key}') definition to the list of styles");
            }
        }

        var currentDictionaries = sourceResources.MergedDictionaries.ToArray();

        foreach (var resourceDictionary in currentDictionaries)
        {
            FindDefaultStyles(context, resourceDictionary, defaultPrefix);
        }
    }

    private class DefaultStylesContext
    {
        public HashSet<string> ParsedDictionaries { get; } = new(StringComparer.CurrentCultureIgnoreCase);

        public List<StyleInfo> Styles { get; } = new();
    }

    private class StyleInfo : IEquatable<StyleInfo>
    {
        public StyleInfo(string sourceKey)
        {
            SourceKey = sourceKey;
        }

        public Type? TargetType { get; set; }

        public ResourceDictionary? SourceDictionary { get; set; }

        public string SourceKey { get; }

        public Style? Style { get; set; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as StyleInfo);
        }

        public bool Equals(StyleInfo? other)
        {
            return other is not null &&
                   SourceKey == other.SourceKey;
        }

        public override int GetHashCode()
        {
            return -1913166433 + EqualityComparer<string>.Default.GetHashCode(SourceKey);
        }

        public override string ToString()
        {
            return $"{TargetType} ({SourceKey} @ {SourceDictionary?.Source})";
        }

        public static bool operator ==(StyleInfo left, StyleInfo right)
        {
            return EqualityComparer<StyleInfo>.Default.Equals(left, right);
        }

        public static bool operator !=(StyleInfo left, StyleInfo right)
        {
            return !(left == right);
        }
    }
}
