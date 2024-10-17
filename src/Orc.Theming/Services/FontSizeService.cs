namespace Orc.Theming;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Catel.Logging;

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

        var mainWindow = Application.Current.MainWindow;
        if (mainWindow is not null)
        {
            TextBlock.SetFontSize(mainWindow, fontSize);
        }

        try
        {
            TextElement.FontSizeProperty.OverrideMetadata(typeof(TextElement), new FrameworkPropertyMetadata(fontSize));
            TextBlock.FontSizeProperty.OverrideMetadata(typeof(TextBlock), new FrameworkPropertyMetadata(fontSize));
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to apply font size");
        }

        RaiseFontSizeChanged();

        return true;
    }

    protected void RaiseFontSizeChanged()
    {
        FontSizeChanged?.Invoke(this, EventArgs.Empty);
    }
}
