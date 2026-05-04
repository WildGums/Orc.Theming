namespace Orc.Theming;

using System;
using System.Windows;
using System.Windows.Controls;
using Catel.Logging;
using Microsoft.Extensions.Logging;
using Orc.Theming.Converters;

public static class ControlExtensions
{
    private static readonly ILogger Logger = LogManager.GetLogger(typeof(ControlExtensions));

    public static TControl GetRequiredTemplateChild<TControl>(this Control control, string partName)
        where TControl : FrameworkElement
    {
        var part = control.Template.FindName(partName, control) as TControl;
        return part
               ?? throw Logger.LogErrorAndCreateException<InvalidOperationException>("Can't find template part '{TemplatePartType}' with name: '{PartName}' in control: '{ControlType}' with name '{ControlName}'", typeof(TControl), partName, control.GetType(), control.Name);
    }
}
