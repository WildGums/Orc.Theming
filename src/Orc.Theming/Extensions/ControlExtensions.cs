namespace Orc.Theming;

using Catel.Logging;
using System.Windows;
using System;
using System.Windows.Controls;

public static class ControlExtensions
{
    private static readonly ILog Log = LogManager.GetCurrentClassLogger();

    public static TControl GetRequiredTemplateChild<TControl>(this Control control, string partName)
        where TControl : FrameworkElement
    {
        var part = control.Template.FindName(partName, control) as TControl;
        return part
               ?? throw Log.ErrorAndCreateException<InvalidOperationException>($"Can't find template part '{typeof(TControl)}' with name: '{partName}' in control: '{control.GetType()}' with name '{control.Name}'");
    }
}
