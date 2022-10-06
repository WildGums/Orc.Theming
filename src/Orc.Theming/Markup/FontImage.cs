﻿namespace Orc.Theming
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Markup;
    using System.Windows.Media;
    using Catel;
    using Catel.Logging;
    using Catel.Windows.Markup;

    /// <summary>
    ///     Markup extension that can show a font as image.
    /// </summary>
    /// <remarks>
    ///     Original idea comes from http://www.codeproject.com/Tips/634540/Using-Font-Icons
    /// </remarks>
    public class FontImage : UpdatableMarkupExtension
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private static readonly Dictionary<string, FontFamily> RegisteredFontFamilies = new Dictionary<string, FontFamily>();
        private static readonly double RenderingEmSize;

        private static readonly HashSet<FontImage> RegisteredFontImages = new HashSet<FontImage>();

        private readonly Dictionary<FrameworkElement, HashSet<DependencyProperty>> _registeredTargetProperties
            = new Dictionary<FrameworkElement, HashSet<DependencyProperty>>();

        static FontImage()
        {
            RegisterFont("Segoe UI Symbol", new FontFamily("Segoe UI Symbol"));

            var dpi = ScreenHelper.GetDpi().Width;
            RenderingEmSize = dpi / 96d;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FontImage" /> class.
        /// </summary>
        public FontImage()
        {
            AllowUpdatableStyleSetters = true;
            FontFamily = DefaultFontFamily;

            RegisteredFontImages.Add(this);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FontImage" /> class.
        /// </summary>
        /// <param name="itemName">Name of the resource.</param>
        public FontImage(string itemName)
            : this()
        {
            ItemName = itemName;
        }

        /// <summary>
        ///     Gets or sets the default name of the font.
        /// </summary>
        /// <value>The default name of the font.</value>
        public static string DefaultFontFamily { get; set; } = "Segoe UI Symbol";

        /// <summary>
        ///     Gets or sets the default brush.
        /// </summary>
        /// <value>The default brush.</value>
        public static Brush DefaultBrush { get; set; } = Brushes.Black;

        /// <summary>
        ///     Gets or sets the default brush key that will be used to determine the brush based on the current theme.
        /// </summary>
        public static string DefaultBrushKey { get; set; } = "Gray1";

        /// <summary>
        ///     Gets the font family.
        /// </summary>
        /// <value>The font family.</value>
        public string? FontFamily { get; set; }

        /// <summary>
        ///     Gets or sets the brush.
        /// </summary>
        /// <value>The brush.</value>
        public Brush? Brush { get; set; }

        /// <summary>
        ///     Gets or sets the brush key that will be used to determine the brush based on the current theme.
        /// </summary>
        public string? BrushKey { get; set; }

        /// <summary>
        ///     Gets or sets the font item name.
        /// </summary>
        /// <value>The font item name.</value>
        [ConstructorArgument("itemName")]
        public string? ItemName { get; set; }

        protected override object? ProvideDynamicValue(IServiceProvider? serviceProvider)
        {
            RegisterTargetProperty();

            return GetImageSource();
        }

        public ImageSource? GetImageSource()
        {
            if (CatelEnvironment.IsInDesignMode)
            {
                return null;
            }

            var itemName = ItemName;
            if (string.IsNullOrEmpty(itemName))
            {
                return null;
            }

            var fontFamily = FontFamily;
            if (fontFamily is null)
            {
                throw Log.ErrorAndCreateException<InvalidOperationException>("FontFamily cannot be null, make sure to set it or use the DefaultFontFamily");
            }

            if (!RegisteredFontFamilies.TryGetValue(fontFamily, out var family))
            {
                throw Log.ErrorAndCreateException<InvalidOperationException>("FontFamily '{0}' is not yet registered, register it first using the RegisterFont method", fontFamily);
            }

            var brush = GetBrush();

            // TODO: Consider caching
            var glyph = CreateGlyph(itemName, family, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal, brush);
            return glyph;
        }

        protected virtual Brush GetBrush()
        {
            // Step 1: specific brush always wins
            var brush = Brush;
            if (brush is not null)
            {
                return brush;
            }

            var currentThemeManager = ThemeManager.Current;
            if (currentThemeManager is null)
            {
                return DefaultBrush;
            }

            // Step 2: respect key
            var brushKey = BrushKey;
            if (!string.IsNullOrWhiteSpace(brushKey))
            {
                brush = currentThemeManager.GetThemeColorBrush(brushKey);
                if (brush is not null)
                {
                    return brush;
                }
            }

            // Step 3: use DefaultBrushKey and search again
            brushKey = DefaultBrushKey;
            if (string.IsNullOrWhiteSpace(brushKey))
            {
                return DefaultBrush;
            }

            brush = currentThemeManager.GetThemeColorBrush(brushKey);
            return brush ?? DefaultBrush;
        }

        private void RegisterTargetProperty()
        {
            if (!(TargetObject is FrameworkElement targetElement))
            {
                return;
            }

            if (!(TargetProperty is DependencyProperty targetProperty))
            {
                return;
            }

            if (!_registeredTargetProperties.TryGetValue(targetElement, out var targetProperties))
            {
                targetProperties = new HashSet<DependencyProperty>();
                _registeredTargetProperties[targetElement] = targetProperties;

                targetElement.Unloaded += OnTargetElementUnloaded;
            }

            targetProperties.Add(targetProperty);
        }

        private void OnTargetElementUnloaded(object? sender, RoutedEventArgs e)
        {
            if (!(sender is FrameworkElement frameworkElement))
            {
                return;
            }

            if (_registeredTargetProperties.ContainsKey(frameworkElement))
            {
                _registeredTargetProperties.Remove(frameworkElement);
            }

            frameworkElement.Unloaded -= OnTargetElementUnloaded;
        }

        private void UnregisterTargetProperties()
        {
            foreach (var registeredTargetProperty in _registeredTargetProperties)
            {
                var frameworkElement = registeredTargetProperty.Key;
                frameworkElement.Unloaded -= OnTargetElementUnloaded;
            }

            _registeredTargetProperties.Clear();
        }

        private void UpdateAllValues()
        {
            foreach (var (frameworkElement, properties) in _registeredTargetProperties)
            {
                foreach (var property in properties)
                {
                    frameworkElement.SetCurrentValue(property, Value);
                }
            }
        }

        private static ImageSource? CreateGlyph(string text, FontFamily fontFamily, FontStyle fontStyle, FontWeight fontWeight, FontStretch fontStretch, Brush foreBrush)
        {
            if (fontFamily is null || string.IsNullOrEmpty(text))
            {
                return null;
            }

            var typeface = new Typeface(fontFamily, fontStyle, fontWeight, fontStretch);

            if (!typeface.TryGetGlyphTypeface(out var glyphTypeface))
            {
                throw Log.ErrorAndCreateException<InvalidOperationException>($"No glyph type face found for font family '{fontFamily.FamilyNames.FirstOrDefault()}'");
            }

            const int NotFoundValue = 42;

            var glyphIndices = new List<ushort>();
            var glyphValues = new List<ushort>();
            var advanceWidths = new List<double>();

            for (var i = 0; i < text.Length; i++)
            {
                glyphIndices.Add((ushort)char.ConvertToUtf32(text, i));
                glyphValues.Add(NotFoundValue);
                advanceWidths.Add(1.0d);

                if (char.IsHighSurrogate(text, i))
                {
                    i++;
                }
            }

            // Validate / replace not found
            for (var i = 0; i < glyphIndices.Count; i++)
            {
                var glyphIndex = glyphIndices[i];

                try
                {
                    if (!glyphTypeface.CharacterToGlyphMap.TryGetValue(glyphIndex, out var glyphValue))
                    {
                        glyphValue = NotFoundValue;
                    }

                    glyphValues[i] = glyphValue;

                    advanceWidths[i] = glyphTypeface.AdvanceWidths[glyphValue] * 1.0;
                }
                catch (Exception)
                {
                    // ignore
                }
            }

            try
            {
#pragma warning disable 618
                var glyphRun = new GlyphRun(glyphTypeface, 0, false, RenderingEmSize, glyphValues, new Point(0, 0), advanceWidths, null, null, null, null, null, null);
#pragma warning restore 618
                var glyphRunDrawing = new GlyphRunDrawing(foreBrush, glyphRun);

                var drawingImage = new DrawingImage(glyphRunDrawing);
                return drawingImage;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generating Glyphrun");
            }

            return null;
        }

        public static void RegisterFont(string name, FontFamily fontFamily)
        {
            Argument.IsNotNullOrWhitespace(() => name);
            ArgumentNullException.ThrowIfNull(fontFamily);

            RegisteredFontFamilies[name] = fontFamily;
        }

        public static IEnumerable<string> GetRegisteredFonts()
        {
            return RegisteredFontFamilies.Keys.ToList();
        }

        public static FontFamily? GetRegisteredFont(string name)
        {
            Argument.IsNotNullOrWhitespace(() => name);

            return RegisteredFontFamilies.ContainsKey(name) ? RegisteredFontFamilies[name] : null;
        }

        private void UpdateBrush()
        {
            var currentThemeManager = ThemeManager.Current;
            if (currentThemeManager is null)
            {
                return;
            }

            UpdateAllValues();
        }

        protected override void OnTargetObjectLoaded()
        {
            base.OnTargetObjectLoaded();

            var currentThemeManager = ThemeManager.Current;
            if (currentThemeManager is null)
            {
                return;
            }

            currentThemeManager.ThemeChanged += OnThemeChanged;

            UpdateBrush();
        }

        protected override void OnTargetObjectUnloaded()
        {
            var currentThemeManager = ThemeManager.Current;
            if (currentThemeManager is null)
            {
                return;
            }

            UnregisterTargetProperties();

            currentThemeManager.ThemeChanged -= OnThemeChanged;

            base.OnTargetObjectUnloaded();
        }

        private void OnThemeChanged(object? sender, EventArgs e)
        {
            UpdateBrush();
        }
    }
}
