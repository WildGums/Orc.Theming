namespace Orc.Theming.Behaviors;

using System;
using System.Windows;
using System.Windows.Controls;
using Catel.Logging;
using Catel.Windows.Interactivity;
using Microsoft.Extensions.Logging;

/// <summary>
/// Behavior that marks a control as a validation adorner site for its parent container.
/// Automatically configures the control to receive validation adorners without requiring name specification.
/// Respects the control's visibility - only active when the control is visible.
/// </summary>
public class ValidationAdornerSiteFor : BehaviorBase<FrameworkElement>
{
    private static readonly ILogger Logger = LogManager.GetLogger(typeof(ValidationAdornerSiteFor));

    /// <summary>
    /// Gets or sets the parent control that this control provides validation adorners for.
    /// If not specified, this control acts as its own validation adorner site.
    /// </summary>
    public FrameworkElement? Element
    {
        get => (FrameworkElement?)GetValue(ElementProperty);
        set => SetValue(ElementProperty, value);
    }

    public static readonly DependencyProperty ElementProperty =
        DependencyProperty.Register(nameof(Element), typeof(FrameworkElement), typeof(ValidationAdornerSiteFor),
            new PropertyMetadata(null, (sender, e) => ((ValidationAdornerSiteFor)sender).Update()));

    protected override void OnAssociatedObjectLoaded()
    {
        base.OnAssociatedObjectLoaded();

        Update();

        // Monitor visibility changes
        AssociatedObject.IsVisibleChanged += OnVisibilityChanged;
    }

    protected override void OnAssociatedObjectUnloaded()
    {
        base.OnAssociatedObjectUnloaded();

        AssociatedObject.IsVisibleChanged -= OnVisibilityChanged;

        ClearValidationAdornerSite();
    }

    /// <summary>
    /// Handles visibility changes of the control.
    /// </summary>
    private void OnVisibilityChanged(object? sender, DependencyPropertyChangedEventArgs e)
    {
        Update();
    }

    private void Update()
    {
        try
        {
            var associatedObject = AssociatedObject;
            if (associatedObject is null)
            {
                return;
            }

            if (associatedObject.IsVisible)
            {
                SetAsValidationAdornerSite();
            }
            else
            {
                ClearValidationAdornerSite();
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Failed to update validation adorner site on visibility change");
        }
    }

    /// <summary>
    /// Sets this control as the validation adorner site.
    /// </summary>
    private void SetAsValidationAdornerSite()
    {
        try
        {
            var target = GetParent();

            Validation.SetValidationAdornerSiteFor(AssociatedObject, target);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Failed to set validation adorner site");
        }
    }

    /// <summary>
    /// Clears the validation adorner site configuration.
    /// </summary>
    private void ClearValidationAdornerSite()
    {
        try
        {
            var target = GetParent();

            Validation.SetValidationAdornerSiteFor(AssociatedObject, null);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Failed to clear validation adorner site");
        }
    }

    private FrameworkElement? GetParent()
    {
        var target = Element ?? AssociatedObject.Parent as FrameworkElement;
        return target;
    }
}
