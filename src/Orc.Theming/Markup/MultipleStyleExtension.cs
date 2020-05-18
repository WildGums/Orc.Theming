namespace Orc.Theming
{
    using System;
    using System.Windows;
    using System.Windows.Markup;

    //souce code was taken here: https://sampathdr.wordpress.com/2014/04/02/wpf-multiple-style-inheritance-using-markup-extension/
    [MarkupExtensionReturnType(typeof(Style))]
    public class MultipleStyleExtension : MarkupExtension
    {
        private readonly string[] _styleKeys;

        public MultipleStyleExtension(string resourceKeys)
        {
            if (string.IsNullOrEmpty(resourceKeys))
            {
                throw new ArgumentNullException(nameof(resourceKeys));
            }

            _styleKeys = resourceKeys.Split('|');

            if (_styleKeys.Length == 0)
            {
                throw new ArgumentException("No input resource keys specified.");
            }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var mergedStyle = new Style();

            foreach (var key in _styleKeys)
            {
                Style style = null;

                try
                {
                    style = new StaticResourceExtension(key).ProvideValue(serviceProvider) as Style;
                }
                catch { }

                if (style == null)
                {
                    throw new InvalidOperationException("Could not find style " + key);
                }

                Merge(mergedStyle, style);
            }

            return mergedStyle;
        }

        private static void Merge(Style style1, Style style2)
        {
            if (style1 == null)
            {
                throw new ArgumentNullException(nameof(style1));
            }

            if (style2 == null)
            {
                throw new ArgumentNullException(nameof(style2));
            }

            if (style1.TargetType.IsAssignableFrom(style2.TargetType))
            {
                style1.TargetType = style2.TargetType;
            }

            if (style2.BasedOn != null)
            {
                Merge(style1, style2.BasedOn);
            }

            // Merge Setters
            foreach (var currentSetter in style2.Setters)
            {
                style1.Setters.Add(currentSetter);
            }

            // Merge Triggers
            foreach (var currentTrigger in style2.Triggers)
            {
                style1.Triggers.Add(currentTrigger);
            }
        }
    }
}
