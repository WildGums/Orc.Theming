namespace Orc.Theming;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using Catel.Logging;
using Catel.Reflection;

public class FontSizeService : IFontSizeService
{
    private static readonly ILog Log = LogManager.GetCurrentClassLogger();

    private double? _fontSize;

    public event EventHandler<EventArgs>? FontSizeChanged;

    public virtual double GetFontSize()
    {
        if (_fontSize.HasValue)
        {
            return _fontSize.Value;
        }

        var mainWindow = Application.Current.MainWindow;
        if (mainWindow is null)
        {
            // default
            return 12d;
        }

        _fontSize = TextBlock.GetFontSize(mainWindow);

        return _fontSize.Value;
    }

    public virtual bool SetFontSize(double fontSize)
    {
        Log.Info($"Setting font size '{fontSize}'");

        _fontSize = fontSize;

        var application = Application.Current;
        if (application is not null)
        {
            // Special case for tooltips: override font size as app resource
            application.Resources["Orc.FontSizes.ToolTip"] = fontSize;
            
            var mainWindow = application.MainWindow;
            if (mainWindow is not null)
            {
                TextBlock.SetFontSize(mainWindow, fontSize);
            }
            else
            {
                application.Activated += OnApplicationActivated;
            }
        }

        try
        {
            OverrideMetadataWithFontSize(TextBlock.FontSizeProperty, typeof(TextBlock), fontSize);
            OverrideMetadataWithFontSize(TextBox.FontSizeProperty, typeof(TextBox), fontSize);
            OverrideMetadataWithFontSize(TextBoxBase.FontSizeProperty, typeof(TextBoxBase), fontSize);
            OverrideMetadataWithFontSize(TextElement.FontSizeProperty, typeof(TextElement), fontSize);
            OverrideMetadataWithFontSize(ToolTip.FontSizeProperty, typeof(ToolTip), fontSize);

        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to apply font size");
        }

        RaiseFontSizeChanged();

        return true;
    }

    private void OnApplicationActivated(object? sender, EventArgs e)
    {
        if (sender is not Application app)
        {
            app = Application.Current;
        }

        var fontSize = _fontSize;
        if (fontSize is null)
        {
            return;
        }

        var mainWindow = app.MainWindow;
        if (mainWindow is null)
        {
            // Come back later
            return;
        }

        app.Activated -= OnApplicationActivated;

        TextBlock.SetFontSize(mainWindow, fontSize.Value);
    }

    protected void RaiseFontSizeChanged()
    {
        FontSizeChanged?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OverrideMetadataWithFontSize(DependencyProperty dependencyProperty, Type targetType, double fontSize)
    {
        try
        {
            var existingMetadata = dependencyProperty.GetMetadata(targetType);
            if (existingMetadata is null)
            {
                dependencyProperty.OverrideMetadata(targetType, new FrameworkPropertyMetadata(fontSize));
                return;
            }

            var dependencyObjectType = DependencyObjectType.FromSystemType(targetType);

            var metadataMapField = dependencyProperty.GetType().GetFieldEx("_metadataMap")!;
            var metadataMap = metadataMapField.GetValue(dependencyProperty)!;

            var itemProperty = metadataMap.GetType().GetPropertyEx("Item")!;
            itemProperty.SetValue(metadataMap, DependencyProperty.UnsetValue, new object?[] { dependencyObjectType.Id });

            // Create new metadata
            var newMetadata = new FrameworkPropertyMetadata(
                fontSize,
                FrameworkPropertyMetadataOptions.Inherits |
                FrameworkPropertyMetadataOptions.Journal |
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsRender,
                existingMetadata?.PropertyChangedCallback,
                existingMetadata?.CoerceValueCallback
            );

            dependencyProperty.OverrideMetadata(targetType, newMetadata);
        }
        catch (Exception ex)
        {
            Log.Error(ex, $"Failed to override font size metadata for '{targetType.Name}'");
        }
    }
}
