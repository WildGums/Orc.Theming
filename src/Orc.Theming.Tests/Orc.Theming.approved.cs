[assembly: System.Resources.NeutralResourcesLanguage("en-US")]
[assembly: System.Runtime.Versioning.TargetFramework(".NETCoreApp,Version=v3.1", FrameworkDisplayName="")]
[assembly: System.Windows.Markup.XmlnsDefinition("http://schemas.wildgums.com/orc/theming", "Orc.Theming")]
[assembly: System.Windows.Markup.XmlnsDefinition("http://schemas.wildgums.com/orc/theming", "Orc.Theming.Views")]
[assembly: System.Windows.Markup.XmlnsPrefix("http://schemas.wildgums.com/orc/theming", "orctheming")]
[assembly: System.Windows.ThemeInfo(System.Windows.ResourceDictionaryLocation.None, System.Windows.ResourceDictionaryLocation.SourceAssembly)]
public static class LoadAssembliesOnStartup { }
public static class ModuleInitializer
{
    public static void Initialize() { }
}
namespace Orc.Theming
{
    public class AccentColorService : Orc.Theming.IAccentColorService
    {
        public AccentColorService() { }
        public event System.EventHandler<System.EventArgs> AccentColorChanged;
        public virtual System.Windows.Media.Color GetAccentColor() { }
        protected void RaiseAccentColorChanged() { }
        public virtual bool SetAccentColor(System.Windows.Media.Color color) { }
    }
    public class BaseColorSchemeService : Orc.Theming.IBaseColorSchemeService
    {
        public BaseColorSchemeService() { }
        public event System.EventHandler<System.EventArgs> BaseColorSchemeChanged;
        public virtual System.Collections.Generic.IReadOnlyList<string> GetAvailableBaseColorSchemes() { }
        public string GetBaseColorScheme() { }
        public bool SetBaseColorScheme(string scheme) { }
    }
    public static class ColorExtensions
    {
        public static System.Windows.Media.SolidColorBrush ToSolidColorBrush(this System.Windows.Media.Color color, double opacity = 1) { }
    }
    public class FontImage : Catel.Windows.Markup.UpdatableMarkupExtension
    {
        public FontImage() { }
        public FontImage(string itemName) { }
        public System.Windows.Media.Brush Brush { get; set; }
        public string FontFamily { get; set; }
        [System.Windows.Markup.ConstructorArgument("itemName")]
        public string ItemName { get; set; }
        public static System.Windows.Media.Brush DefaultBrush { get; set; }
        public static string DefaultFontFamily { get; set; }
        public System.Windows.Media.ImageSource GetImageSource() { }
        protected override object ProvideDynamicValue(System.IServiceProvider serviceProvider) { }
        public static System.Windows.Media.FontFamily GetRegisteredFont(string name) { }
        public static System.Collections.Generic.IEnumerable<string> GetRegisteredFonts() { }
        public static void RegisterFont(string name, System.Windows.Media.FontFamily fontFamily) { }
    }
    public static class FrameworkElementExtensions
    {
        public static TBehavior AttachBehavior<TBehavior>(this System.Windows.FrameworkElement frameworkElement)
            where TBehavior : Microsoft.Xaml.Behaviors.Behavior { }
        public static void DetachBehavior<TBehavior>(this System.Windows.FrameworkElement frameworkElement)
            where TBehavior : Microsoft.Xaml.Behaviors.Behavior { }
    }
    public interface IAccentColorService
    {
        event System.EventHandler<System.EventArgs> AccentColorChanged;
        System.Windows.Media.Color GetAccentColor();
        bool SetAccentColor(System.Windows.Media.Color color);
    }
    public interface IBaseColorSchemeService
    {
        event System.EventHandler<System.EventArgs> BaseColorSchemeChanged;
        System.Collections.Generic.IReadOnlyList<string> GetAvailableBaseColorSchemes();
        string GetBaseColorScheme();
        bool SetBaseColorScheme(string scheme);
    }
    public interface IResourceDictionaryService
    {
        bool IsResourceDictionaryAvailable(string resourceDictionaryUri);
    }
    public interface IThemeService
    {
        Orc.Theming.ThemeInfo GetThemeInfo();
        bool ShouldCreateStyleForwarders();
    }
    public class LibraryThemeProvider : ControlzEx.Theming.LibraryThemeProvider
    {
        public LibraryThemeProvider() { }
        public override void FillColorSchemeValues(System.Collections.Generic.Dictionary<string, string> values, ControlzEx.Theming.RuntimeThemeColorValues colorValues) { }
    }
    public class ResourceDictionaryService : Orc.Theming.IResourceDictionaryService
    {
        public ResourceDictionaryService() { }
        public virtual bool IsResourceDictionaryAvailable(string resourceDictionaryUri) { }
    }
    public static class ScreenHelper
    {
        public static System.Windows.Size GetDpi() { }
    }
    public static class StyleHelper
    {
        public static bool IsStyleForwardingEnabled { get; }
        public static void CreateStyleForwardersForDefaultStyles(string defaultPrefix = "Default") { }
        public static void CreateStyleForwardersForDefaultStyles(System.Windows.ResourceDictionary sourceResources, string defaultPrefix = "Default") { }
        public static void CreateStyleForwardersForDefaultStyles(System.Windows.ResourceDictionary sourceResources, System.Windows.ResourceDictionary targetResources, string defaultPrefix = "Default") { }
        [MethodTimer.Time]
        public static void CreateStyleForwardersForDefaultStyles(System.Windows.ResourceDictionary rootResourceDictionary, System.Windows.ResourceDictionary sourceResources, System.Windows.ResourceDictionary targetResources, string defaultPrefix = "Default") { }
        public static void EnsureApplicationResourcesAndCreateStyleForwarders(System.Uri applicationResourceDictionary, string defaultPrefix = "Default") { }
    }
    public class ThemeColor : Catel.Windows.Markup.UpdatableMarkupExtension
    {
        public ThemeColor() { }
        public ThemeColor(Orc.Theming.ThemeColorStyle themeColorStyle) { }
        public string ResourceName { get; set; }
        public Orc.Theming.ThemeColorStyle ThemeColorStyle { get; set; }
        protected override void OnTargetObjectLoaded() { }
        protected override void OnTargetObjectUnloaded() { }
        protected override object ProvideDynamicValue(System.IServiceProvider serviceProvider) { }
    }
    public class ThemeColorBrush : Catel.Windows.Markup.UpdatableMarkupExtension
    {
        public ThemeColorBrush() { }
        public ThemeColorBrush(Orc.Theming.ThemeColorStyle themeColorStyle) { }
        public string ResourceName { get; set; }
        public Orc.Theming.ThemeColorStyle ThemeColorStyle { get; set; }
        protected override void OnTargetObjectLoaded() { }
        protected override void OnTargetObjectUnloaded() { }
        protected override object ProvideDynamicValue(System.IServiceProvider serviceProvider) { }
    }
    public enum ThemeColorStyle
    {
        AccentColor = 0,
        AccentColor80 = 1,
        AccentColor60 = 2,
        AccentColor40 = 3,
        AccentColor20 = 4,
        [System.Obsolete("Use `AccentColor80` instead. Will be removed in version 5.0.0.", true)]
        AccentColor1 = 1,
        [System.Obsolete("Use `AccentColor60` instead. Will be removed in version 5.0.0.", true)]
        AccentColor2 = 2,
        [System.Obsolete("Use `AccentColor40` instead. Will be removed in version 5.0.0.", true)]
        AccentColor3 = 3,
        [System.Obsolete("Use `AccentColor20` instead. Will be removed in version 5.0.0.", true)]
        AccentColor4 = 4,
        BorderColor = 5,
        BackgroundColor = 6,
        ForegroundColor = 7,
        HighlightColor = 8,
        Gray1 = 9,
        Gray2 = 10,
        Gray3 = 11,
        Gray4 = 12,
        Gray5 = 13,
        Gray6 = 14,
        Gray7 = 15,
        Gray8 = 16,
        Gray9 = 17,
        Gray10 = 18,
        Text = 19,
        DefaultBorder = 5,
        DefaultBackground = 6,
        DefaultForeground = 7,
        DefaultText = 19,
        CheckedBorder = 8,
        CheckedBackground = 4,
        CheckedForeground = 7,
        CheckedText = 19,
        CheckedMouseOverBorder = 2,
        CheckedMouseOverBackground = 4,
        CheckedMouseOverForeground = 7,
        CheckedMouseOverText = 19,
        MouseOverBorder = 3,
        MouseOverBackground = 4,
        MouseOverForeground = 7,
        MouseOverText = 19,
        PressedBorder = 2,
        PressedBackground = 3,
        PressedForeground = 7,
        PressedText = 19,
        DisabledBorder = 13,
        DisabledBackground = 3,
        DisabledForeground = 7,
        DisabledText = 19,
        HighlightedBorder = 2,
        HighlightedBackground = 3,
        HighlightedForeground = 7,
        HighlightedText = 19,
        SelectionActiveBorder = 1,
        SelectionActiveBackground = 2,
        SelectionActiveForeground = 7,
        SelectionActiveText = 19,
        SelectionInactiveBorder = 3,
        SelectionInactiveBackground = 4,
        SelectionInactiveForeground = 7,
        SelectionInactiveText = 19,
    }
    public class ThemeInfo
    {
        public ThemeInfo() { }
        public System.Windows.Media.Color AccentBaseColor { get; set; }
        public string BaseColorScheme { get; set; }
        public System.Windows.Media.Color HighlightColor { get; set; }
    }
    public class ThemeManager
    {
        public ThemeManager(ControlzEx.Theming.ThemeManager controlzThemeManager, Orc.Theming.IAccentColorService accentColorService, Orc.Theming.IBaseColorSchemeService baseColorSchemeService) { }
        public static Orc.Theming.ThemeManager Current { get; }
        public System.Windows.Media.SolidColorBrush GetAccentColorBrush() { }
        public System.Windows.Media.Color GetThemeColor(Orc.Theming.ThemeColorStyle colorStyle = 0) { }
        public System.Windows.Media.Color GetThemeColor(string resourceName) { }
        public System.Windows.Media.SolidColorBrush GetThemeColorBrush(Orc.Theming.ThemeColorStyle colorStyle = 0) { }
        public System.Windows.Media.SolidColorBrush GetThemeColorBrush(string resourceName) { }
        public virtual void SynchronizeTheme() { }
    }
    public class ThemeService : Orc.Theming.IThemeService
    {
        public ThemeService(Orc.Theming.IAccentColorService accentColorService, Orc.Theming.IBaseColorSchemeService baseColorSchemeService) { }
        public virtual Orc.Theming.ThemeInfo GetThemeInfo() { }
        public virtual bool ShouldCreateStyleForwarders() { }
    }
}
namespace Orc.Theming.ViewModels
{
    public class AccentColorSwitcherViewModel : Catel.MVVM.ViewModelBase
    {
        public static readonly Catel.Data.PropertyData AccentColorsProperty;
        public static readonly Catel.Data.PropertyData SelectedAccentColorProperty;
        public AccentColorSwitcherViewModel(Orc.Theming.ThemeManager themeManager, Orc.Theming.IAccentColorService accentColorService) { }
        public System.Collections.Generic.List<System.Windows.Media.Color> AccentColors { get; }
        public System.Windows.Media.Color SelectedAccentColor { get; set; }
        protected override System.Threading.Tasks.Task CloseAsync() { }
        protected override System.Threading.Tasks.Task InitializeAsync() { }
    }
    public class BaseColorSchemeSwitcherViewModel : Catel.MVVM.ViewModelBase
    {
        public static readonly Catel.Data.PropertyData BaseColorSchemesProperty;
        public static readonly Catel.Data.PropertyData SelectedBaseColorSchemeProperty;
        public BaseColorSchemeSwitcherViewModel(Orc.Theming.ThemeManager themeManager, Orc.Theming.IBaseColorSchemeService baseColorSchemeService) { }
        public System.Collections.Generic.IReadOnlyList<string> BaseColorSchemes { get; }
        public string SelectedBaseColorScheme { get; set; }
        protected override System.Threading.Tasks.Task CloseAsync() { }
        protected override System.Threading.Tasks.Task InitializeAsync() { }
    }
    public class ThemeSwitcherViewModel : Catel.MVVM.ViewModelBase
    {
        public static readonly Catel.Data.PropertyData AccentColorsProperty;
        public static readonly Catel.Data.PropertyData BaseColorSchemesProperty;
        public static readonly Catel.Data.PropertyData SelectedAccentColorProperty;
        public static readonly Catel.Data.PropertyData SelectedBaseColorSchemeProperty;
        public ThemeSwitcherViewModel(Orc.Theming.IAccentColorService accentColorService, Orc.Theming.IBaseColorSchemeService baseColorSchemeService) { }
        public System.Collections.Generic.List<System.Windows.Media.Color> AccentColors { get; }
        public System.Collections.Generic.IReadOnlyList<string> BaseColorSchemes { get; }
        public System.Windows.Media.Color SelectedAccentColor { get; set; }
        public string SelectedBaseColorScheme { get; set; }
        protected override System.Threading.Tasks.Task CloseAsync() { }
        protected override System.Threading.Tasks.Task InitializeAsync() { }
    }
}
namespace Orc.Theming.Views
{
    public class AccentColorSwitcherView : Catel.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector
    {
        public AccentColorSwitcherView() { }
        public void InitializeComponent() { }
    }
    public class BaseColorSchemeSwitcherView : Catel.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector
    {
        public BaseColorSchemeSwitcherView() { }
        public void InitializeComponent() { }
    }
    public class ThemeSwitcherView : Catel.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector
    {
        public ThemeSwitcherView() { }
        public void InitializeComponent() { }
    }
}