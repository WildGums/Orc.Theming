namespace Orc.Theming.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Catel;
    using Catel.IoC;

    public partial class ThemeImage : UserControl
    {
        private const string BaseColorScheme = "{basecolorscheme}";

        private readonly IBaseColorSchemeService _baseColorSchemeService;

        private bool _isSubscribed;

        public ThemeImage()
        {
            InitializeComponent();

            var serviceLocator = ServiceLocator.Default;

            _baseColorSchemeService = serviceLocator.TryResolveType<IBaseColorSchemeService>();

            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(nameof(Source), 
            typeof(string), typeof(ThemeImage), new PropertyMetadata(null, (sender, e) => ((ThemeImage)sender).OnSourceChanged()));


        public Stretch Stretch
        {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }

        public static readonly DependencyProperty StretchProperty = DependencyProperty.Register(nameof(Stretch), 
            typeof(Stretch), typeof(ThemeImage), new PropertyMetadata(Stretch.None));


        public StretchDirection StretchDirection
        {
            get { return (StretchDirection)GetValue(StretchDirectionProperty); }
            set { SetValue(StretchDirectionProperty, value); }
        }

        public static readonly DependencyProperty StretchDirectionProperty = DependencyProperty.Register(nameof(StretchDirection), 
            typeof(StretchDirection), typeof(ThemeImage), new PropertyMetadata(StretchDirection.Both));


        private void OnSourceChanged()
        {
            UpdateSource();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!_isSubscribed)
            {
                _isSubscribed = true;

                if (_baseColorSchemeService is not null)
                {
                    _baseColorSchemeService.BaseColorSchemeChanged += OnBaseColorSchemeServiceBaseColorSchemeChanged;
                }
            }
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            if (_isSubscribed)
            {
                _isSubscribed = false;

                if (_baseColorSchemeService is not null)
                {
                    _baseColorSchemeService.BaseColorSchemeChanged -= OnBaseColorSchemeServiceBaseColorSchemeChanged;
                }
            }
        }

        private void OnBaseColorSchemeServiceBaseColorSchemeChanged(object sender, EventArgs e)
        {
            UpdateSource();
        }

        private void UpdateSource()
        {
            var finalSource = Source;
            if (finalSource is not null)
            {
                var ready = false;
                var baseColorScheme = _baseColorSchemeService.GetBaseColorScheme()?.ToLower() ?? string.Empty;

                // Step 1: replace fixed item
                var index = finalSource.IndexOfIgnoreCase(BaseColorScheme);
                if (index >= 0)
                {
                    finalSource = finalSource.Remove(index, BaseColorScheme.Length);
                    finalSource = finalSource.Insert(index, baseColorScheme);

                    ready = true;
                }

                // Step 2: if we haven't replaced, auto replace
                if (!ready)
                {
                    var lastDotIndex = finalSource.LastIndexOf('.');
                    if (lastDotIndex >= 0)
                    {
                        finalSource = finalSource.Insert(lastDotIndex, $".{baseColorScheme}");
                    }
                }
            }

            ImageSource imageSource = null;

            if (finalSource is not null)
            {
                imageSource = new BitmapImage(new Uri(finalSource, UriKind.RelativeOrAbsolute));
            }

            Image.SetCurrentValue(Image.SourceProperty, imageSource);
        }
    }
}
