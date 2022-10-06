namespace Orc.Theming
{
    using System.Linq;
    using System.Windows;
    using Catel;
    using Catel.IoC;
    using Microsoft.Xaml.Behaviors;

    public static class FrameworkElementExtensions
    {
        public static TBehavior AttachBehavior<TBehavior>(this FrameworkElement frameworkElement)
            where TBehavior : Behavior
        {
            Argument.IsNotNull(() => frameworkElement);

            var behaviors = Interaction.GetBehaviors(frameworkElement);

            var existingBehaviorOfType = behaviors.OfType<TBehavior>().FirstOrDefault();
            if (existingBehaviorOfType is not null)
            {
                return existingBehaviorOfType;
            }

#pragma warning disable IDISP004 // Don't ignore created IDisposable
            var behavior = frameworkElement.GetTypeFactory().CreateRequiredInstanceWithParametersAndAutoCompletion<TBehavior>();
#pragma warning restore IDISP004 // Don't ignore created IDisposable
            behaviors.Add(behavior);

            return behavior;
        }

        public static void DetachBehavior<TBehavior>(this FrameworkElement frameworkElement)
            where TBehavior : Behavior
        {
            Argument.IsNotNull(() => frameworkElement);

            var behaviors = Interaction.GetBehaviors(frameworkElement);

            var detachingBehavior = behaviors.OfType<TBehavior>().FirstOrDefault();
            if (detachingBehavior is null)
            {
                return;
            }

            behaviors.Remove(detachingBehavior);
        }
    }
}
