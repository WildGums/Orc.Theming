namespace Orc.Theming;

using System;
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
    private static readonly ILog Log = LogManager.GetCurrentClassLogger();

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
    /// Gets or sets whether this markup extension should subscribe to events to be responsive.
    /// <para />
    /// The default value is <c>false</c> for performance reasons.
    /// </summary>
    /// <value>The font size delta.</value>
    [ConstructorArgument("subscribeToEvents")]
    public bool SubscribeToEvents { get; set; }

    protected override object? ProvideDynamicValue(IServiceProvider? serviceProvider)
    {
        var defaultFontSize = 12d;

        if (TargetObject is DependencyObject dependencyObject)
        {
            var parentControl = dependencyObject.GetLogicalParent()?.FindLogicalAncestorByType<Control>();
            if (parentControl is not null)
            {
                defaultFontSize = (double)parentControl.GetValue(Control.FontSizeProperty);
            }
        }

        var finalFontSize = defaultFontSize;

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

    protected override void OnTargetObjectLoaded()
    {
        base.OnTargetObjectLoaded();

        if (SubscribeToEvents)
        {
            var fontSizeService = _fontSizeService;
            if (fontSizeService is null)
            {
                _fontSizeService = fontSizeService = ServiceLocator.Default.ResolveType<IFontSizeService>();
            }

            if (fontSizeService is not null)
            {
                fontSizeService.FontSizeChanged += OnFontSizeServiceFontSizeChanged;
            }
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

    private void OnFontSizeServiceFontSizeChanged(object? sender, EventArgs e)
    {
        UpdateValue();
    }
}
