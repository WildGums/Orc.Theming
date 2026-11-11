namespace Orc.Theming.Behaviors;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Catel.Logging;
using Catel.Windows;
using Catel.Windows.Interactivity;
using Microsoft.Extensions.Logging;

/// <summary>
/// Behavior that configures validation adorner placement on a child control.
/// Sets Validation.ValidationAdornerSite to anchor validation adorners to a specific element.
/// </summary>
public class ValidationAdornerSite : BehaviorBase<ContentControl>
{
    private static readonly ILogger Logger = LogManager.GetLogger(typeof(ValidationAdornerSite));

    /// <summary>
    /// Gets or sets the name of the child control to use as the validation adorner site.
    /// Validation adorners will be anchored to this control.
    /// </summary>
    public string? ValidationAdornerSiteName
    {
        get => (string?)GetValue(ValidationAdornerSiteNameProperty);
        set => SetValue(ValidationAdornerSiteNameProperty, value);
    }

    public static readonly DependencyProperty ValidationAdornerSiteNameProperty =
        DependencyProperty.Register(nameof(ValidationAdornerSiteName), typeof(string), typeof(ValidationAdornerSite),
            new PropertyMetadata(null, (sender, e) => ((ValidationAdornerSite)sender).OnValidationAdornerSiteNameChanged((string?)e.NewValue)));


    protected override void OnAssociatedObjectLoaded()
    {
        base.OnAssociatedObjectLoaded();

        if (!string.IsNullOrEmpty(ValidationAdornerSiteName))
        {
            OnValidationAdornerSiteNameChanged(ValidationAdornerSiteName);
        }
    }

    /// <summary>
    /// Handles changes to the ValidationAdornerSiteName property.
    /// </summary>
    private void OnValidationAdornerSiteNameChanged(string? siteName)
    {
        UpdateValidationAdornerSite(AssociatedObject, siteName);
    }

    /// <summary>
    /// Configures the validation adorner site by finding the child control and setting Validation.ValidationAdornerSite.
    /// </summary>
    private static void UpdateValidationAdornerSite(ContentControl? element, string? siteName)
    {
        try
        {
            if (element is null || string.IsNullOrEmpty(siteName))
            {
                return;
            }

            var adornerSite = element.FindVisualDescendantByName(siteName);
            if (adornerSite is null)
            {
                Logger.LogWarning("Could not find validation adorner site control '{ControlName}' in visual tree", siteName);
                return;
            }

            Validation.SetValidationAdornerSite(element, adornerSite);
            //Validation.SetValidationAdornerSiteFor(element, adornerSite);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Failed to set validation adorner site");
        }
    }
}
