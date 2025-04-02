namespace Orc.Theming;

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Catel.IoC;
using Catel.Logging;
using Catel.Windows;
using Catel.Windows.Markup;

/// <summary>
/// Markup extension that can set a relative font size.
/// </summary>
public class FontSize : UpdatableMarkupExtension
{
    private const double DefaultFontSize = 12d;

    private static readonly ILog Log = LogManager.GetCurrentClassLogger();

    private static readonly Stopwatch LastUpdatedTextBlockFontSizeStopwatch = Stopwatch.StartNew();
    private static double? DefaultTextBlockFontSize = null;

    private IFontSizeService? _fontSizeService;


    static FontSize()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FontSize" /> class.
    /// </summary>
    public FontSize()
    {
        Scale = 1.0d;
        SubscribeToEvents = false;
        Mode = FontSizeMode.Default;
    }

    public FontSize(double absolute)
        : this()
    {
        Absolute = absolute;
    }

    /// <summary>
    /// Gets or sets the font size scaling.
    /// </summary>
    /// <value>The font size scaling.</value>
    [ConstructorArgument("scale")]
    public double? Scale { get; set; }

    /// <summary>
    /// Gets or sets the font size delta.
    /// </summary>
    /// <value>The font size delta.</value>
    [ConstructorArgument("delta")]
    public double? Delta { get; set; }

    /// <summary>
    /// Gets or sets the absolute font size, that will be scaled to the default font size of 12.
    /// </summary>
    /// <value>The absolute font size.</value>
    [ConstructorArgument("absolute")]
    public double? Absolute { get; set; }

    /// <summary>
    /// Gets or sets the mode
    /// </summary>
    [ConstructorArgument("mode")]
    public FontSizeMode Mode { get; set; }

    /// <summary>
    /// Gets or sets the key of the resource to use when calculating the value.
    /// </summary>
    [ConstructorArgument("resourceKey")]
    public string? ResourceKey { get; set; }

    /// <summary>
    /// Gets or sets whether this markup extension should subscribe to events to be responsive.
    /// <para />
    /// The default value is <c>false</c> for performance reasons.
    /// </summary>
    /// <value>The font size delta.</value>
    [ConstructorArgument("subscribeToEvents")]
    public bool SubscribeToEvents { get; set; }

    /// <summary>
    /// Resets the cache by re-evaluating the default font size.
    /// </summary>
    public static void ResetCache()
    {
        Debug.WriteLine("Resetting default TextBlock font size");

        DefaultTextBlockFontSize = null;
        LastUpdatedTextBlockFontSizeStopwatch.Reset();
    }

    /// <summary>
    /// Gets the font size using the <see cref="FontSizeMode.TextBlockMetadata"/> mode.
    /// </summary>
    /// <param name="absolute">The absolute value based on the standard 12p font size.</param>
    /// <returns></returns>
    public static double GetFontSize(double absolute)
    {
        var defaultFontSize = GetFontSizeFromTextBlockMetadata();

        return GetFontSize(defaultFontSize, absolute);
    }

    public static double GetFontSize(double defaultFontSize, double absoluteFontSize)
    {
        // Use constant to define the ratio
        var factor = absoluteFontSize / DefaultFontSize;
        var finalFontSize = defaultFontSize * factor;
        return finalFontSize;
    }

    protected override object? ProvideDynamicValue(IServiceProvider? serviceProvider)
    {
        var defaultFontSize = DefaultFontSize;

        var mode = Mode;

        // Enforce resource mode when a resource name is available
        var resourceKey = ResourceKey;
        if (!string.IsNullOrWhiteSpace(resourceKey))
        {
            mode = FontSizeMode.Resource;
        }

        switch (mode)
        {
            case FontSizeMode.TextBlockMetadata:
                defaultFontSize = GetFontSizeFromTextBlockMetadata();
                break;

            case FontSizeMode.Parent:
                defaultFontSize = GetFontSizeFromParent();
                break;

            case FontSizeMode.Service:
                defaultFontSize = GetFontSizeFromService();
                break;

            case FontSizeMode.Resource:
                defaultFontSize = GetFontSizeFromResource();
                break;

            default:
                throw Log.ErrorAndCreateException<NotSupportedException>($"Mode '{Mode}' is not supported");
        }

        var finalFontSize = defaultFontSize;

        if (Absolute is not null)
        {
            // Return immediately, always use 12d as base font
            return GetFontSize(Absolute.Value);
        }

        if (Delta is not null)
        {
            finalFontSize += Delta.Value;
        }

        if (Scale is not null)
        {
            finalFontSize *= Scale.Value;
        }

        return finalFontSize;
    }

    protected static double GetFontSizeFromTextBlockMetadata()
    {
        var defaultValue = DefaultTextBlockFontSize;
        if (defaultValue is null)
        {
            defaultValue = DefaultTextBlockFontSize = (double)TextBlock.FontSizeProperty.GetMetadata(typeof(TextBlock)).DefaultValue;
        }

        return defaultValue.Value;
    }

    protected virtual double GetFontSizeFromParent()
    {
        var defaultFontSize = DefaultFontSize;

        if (TargetObject is DependencyObject dependencyObject)
        {
            var parentControl = dependencyObject.GetLogicalParent()?.FindLogicalAncestorByType<Control>();
            if (parentControl is not null)
            {
                defaultFontSize = (double)parentControl.GetValue(Control.FontSizeProperty);
            }
        }

        return defaultFontSize;
    }

    protected virtual double GetFontSizeFromService()
    {
        var service = GetFontSizeService();
        return service.GetFontSize();
    }

    protected virtual double GetFontSizeFromResource()
    {
        var resourceKey = ResourceKey;
        if (!string.IsNullOrWhiteSpace(resourceKey))
        {
            if (TargetObject is DependencyObject dependencyObject)
            {
                var frameworkElement = dependencyObject.FindLogicalOrVisualAncestorByType<FrameworkElement>();
                if (frameworkElement is not null)
                {
                    var resource = frameworkElement.TryFindResource(resourceKey);
                    if (resource is double doubleValue)
                    {
                        return doubleValue;
                    }
                }
            }

            // Fallback to application
            var application = Application.Current;
            if (application is not null)
            {
                var resource = application.TryFindResource(resourceKey);
                if (resource is double doubleValue)
                {
                    return doubleValue;
                }
            }
        }

        // TODO: Based on discussion and performance, we consider
        // resolving "fixed resources" based on the properties (e.g. Delta=4 resolves to Heading2)

        throw Log.ErrorAndCreateException<NotImplementedException>("Reserved for future usage");
    }

    protected override void OnTargetObjectLoaded()
    {
        base.OnTargetObjectLoaded();

        if (SubscribeToEvents)
        {
            var fontSizeService = GetFontSizeService();
            fontSizeService.FontSizeChanged += OnFontSizeServiceFontSizeChanged;
        }
    }

    protected override void OnTargetObjectUnloaded()
    {
        var fontSizeService = _fontSizeService;
        if (fontSizeService is not null)
        {
            fontSizeService.FontSizeChanged -= OnFontSizeServiceFontSizeChanged;
        }

        base.OnTargetObjectUnloaded();
    }

    private IFontSizeService GetFontSizeService()
    {
        var fontSizeService = _fontSizeService;
        if (fontSizeService is null)
        {
            _fontSizeService = fontSizeService = ServiceLocator.Default.ResolveRequiredType<IFontSizeService>();
        }

        return fontSizeService;
    }

    private void OnFontSizeServiceFontSizeChanged(object? sender, EventArgs e)
    {
        // "Simple" way for fast static caching with single updates
        if (LastUpdatedTextBlockFontSizeStopwatch.ElapsedMilliseconds > 500)
        {
            ResetCache();
        }

        UpdateValue();
    }
}
