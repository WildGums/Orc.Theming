namespace Orc.Theming;

using System;
using System.Linq;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xaml.Behaviors;

public static class FrameworkElementExtensions
{
    public static TBehavior AttachBehavior<TBehavior>(this FrameworkElement frameworkElement)
        where TBehavior : Behavior
    {
        ArgumentNullException.ThrowIfNull(frameworkElement);

        var behaviors = Interaction.GetBehaviors(frameworkElement);

        var existingBehaviorOfType = behaviors.OfType<TBehavior>().FirstOrDefault();
        if (existingBehaviorOfType is not null)
        {
            return existingBehaviorOfType;
        }

        var serviceProvider = Catel.IoC.IoCContainer.ServiceProvider;
        var behavior = ActivatorUtilities.CreateInstance<TBehavior>(serviceProvider);
        behaviors.Add(behavior);

        return behavior;
    }

    public static void DetachBehavior<TBehavior>(this FrameworkElement frameworkElement)
        where TBehavior : Behavior
    {
        ArgumentNullException.ThrowIfNull(frameworkElement);

        var behaviors = Interaction.GetBehaviors(frameworkElement);

        var detachingBehavior = behaviors.OfType<TBehavior>().FirstOrDefault();
        if (detachingBehavior is null)
        {
            return;
        }

        behaviors.Remove(detachingBehavior);
    }
}
