[assembly: System.Resources.NeutralResourcesLanguageAttribute("en-US")]
[assembly: System.Runtime.Versioning.TargetFrameworkAttribute(".NETFramework,Version=v4.6", FrameworkDisplayName=".NET Framework 4.6")]
[assembly: System.Windows.Markup.XmlnsDefinitionAttribute("http://schemas.wildgums.com/orc/theming", "Orc.Theming")]
[assembly: System.Windows.Markup.XmlnsPrefixAttribute("http://schemas.wildgums.com/orc/theming", "orctheming")]
[assembly: System.Windows.ThemeInfoAttribute(System.Windows.ResourceDictionaryLocation.None, System.Windows.ResourceDictionaryLocation.SourceAssembly)]
public class static LoadAssembliesOnStartup { }
public class static ModuleInitializer
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
        public virtual void SetAccentColor(System.Windows.Media.Color color) { }
    }
    public class BaseColorSchemeService : Orc.Theming.IBaseColorSchemeService
    {
        public BaseColorSchemeService() { }
        public event System.EventHandler<System.EventArgs> BaseColorSchemeChanged;
        public virtual System.Collections.Generic.IReadOnlyList<string> GetAvailableBaseColorSchemes() { }
        public string GetBaseColorScheme() { }
        public bool SetBaseColorScheme(string color) { }
    }
    public class static ColorExtensions
    {
        public static System.Windows.Media.SolidColorBrush ToSolidColorBrush(this System.Windows.Media.Color color, double opacity = 1) { }
    }
    public class FontImage : Catel.Windows.Markup.UpdatableMarkupExtension
    {
        public FontImage() { }
        public FontImage(string itemName) { }
        public System.Windows.Media.Brush Brush { get; set; }
        public static System.Windows.Media.Brush DefaultBrush { get; set; }
        public static string DefaultFontFamily { get; set; }
        public string FontFamily { get; set; }
        [System.Windows.Markup.ConstructorArgumentAttribute("itemName")]
        public string ItemName { get; set; }
        public System.Windows.Media.ImageSource GetImageSource() { }
        public static System.Windows.Media.FontFamily GetRegisteredFont(string name) { }
        public static System.Collections.Generic.IEnumerable<string> GetRegisteredFonts() { }
        protected override object ProvideDynamicValue(System.IServiceProvider serviceProvider) { }
        public static void RegisterFont(string name, System.Windows.Media.FontFamily fontFamily) { }
    }
    public interface IAccentColorService
    {
        public event System.EventHandler<System.EventArgs> AccentColorChanged;
        System.Windows.Media.Color GetAccentColor();
        void SetAccentColor(System.Windows.Media.Color color);
    }
    public interface IBaseColorSchemeService
    {
        public event System.EventHandler<System.EventArgs> BaseColorSchemeChanged;
        System.Collections.Generic.IReadOnlyList<string> GetAvailableBaseColorSchemes();
        string GetBaseColorScheme();
        bool SetBaseColorScheme(string color);
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
        public static readonly ControlzEx.Theming.LibraryThemeProvider DefaultInstance;
        public LibraryThemeProvider() { }
        public override void FillColorSchemeValues(System.Collections.Generic.Dictionary<string, string> values, ControlzEx.Theming.RuntimeThemeColorValues colorValues) { }
    }
    public class ResourceDictionaryService : Orc.Theming.IResourceDictionaryService
    {
        public ResourceDictionaryService() { }
        public virtual bool IsResourceDictionaryAvailable(string resourceDictionaryUri) { }
    }
    public class static ScreenHelper
    {
        public static System.Windows.Size GetDpi() { }
    }
    public class static StyleHelper
    {
        public static bool IsStyleForwardingEnabled { get; }
        public static void CreateStyleForwardersForDefaultStyles(string defaultPrefix = "Default") { }
        public static void CreateStyleForwardersForDefaultStyles(System.Windows.ResourceDictionary sourceResources, string defaultPrefix = "Default") { }
        public static void CreateStyleForwardersForDefaultStyles(System.Windows.ResourceDictionary sourceResources, System.Windows.ResourceDictionary targetResources, string defaultPrefix = "Default") { }
        [MethodTimer.TimeAttribute()]
        public static void CreateStyleForwardersForDefaultStyles(System.Windows.ResourceDictionary rootResourceDictionary, System.Windows.ResourceDictionary sourceResources, System.Windows.ResourceDictionary targetResources, string defaultPrefix = "Default") { }
        public static void EnsureApplicationResourcesAndCreateStyleForwarders(System.Uri applicationResourceDictionary, string defaultPrefix = "Default") { }
    }
    public class ThemeColor : Catel.Windows.Markup.UpdatableMarkupExtension
    {
        public ThemeColor() { }
        public ThemeColor(Orc.Theming.ThemeColorStyle themeColorStyle) { }
        public Orc.Theming.ThemeColorStyle ThemeColorStyle { get; set; }
        protected override void OnTargetObjectLoaded() { }
        protected override void OnTargetObjectUnloaded() { }
        protected override object ProvideDynamicValue(System.IServiceProvider serviceProvider) { }
    }
    public class ThemeColorBrush : Catel.Windows.Markup.UpdatableMarkupExtension
    {
        public ThemeColorBrush() { }
        public ThemeColorBrush(Orc.Theming.ThemeColorStyle themeColorStyle) { }
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
        AccentColor1 = 1,
        AccentColor2 = 2,
        AccentColor3 = 3,
        AccentColor4 = 4,
        AccentColor5 = 5,
        BorderColor = 6,
        BorderColor80 = 7,
        BorderColor60 = 8,
        BorderColor40 = 9,
        BorderColor20 = 10,
        BorderColor1 = 7,
        BorderColor2 = 8,
        BorderColor3 = 9,
        BorderColor4 = 10,
        BorderColor5 = 11,
        BackgroundColor = 12,
        ForegroundColor = 13,
        ForegroundAlternativeColor = 14,
        DarkHighlight = 3,
        Highlight = 4,
        BorderLight = 9,
        BorderMedium = 8,
        BorderDark = 7,
        BorderMouseOver = 1,
        BorderPressed = 0,
        BorderChecked = 0,
        BorderSelected = 0,
        BorderSelectedInactive = 2,
        BorderDisabled = 11,
        BackgroundMouseOver = 4,
        BackgroundPressed = 3,
        BackgroundChecked = 0,
        BackgroundSelected = 3,
        BackgroundSelectedInactive = 4,
        BackgroundDisabled = 5,
        ForegroundMouseOver = 13,
        ForegroundPressed = 14,
        ForegroundChecked = 14,
        ForegroundSelected = 14,
        ForegroundSelectedInactive = 14,
        ForegroundDisabled = 13,
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
        public static Orc.Theming.ThemeManager Current { get; set; }
        public System.Windows.Media.SolidColorBrush GetAccentColorBrush() { }
        public System.Windows.Media.Color GetThemeColor(Orc.Theming.ThemeColorStyle colorStyle = 0) { }
        public System.Windows.Media.SolidColorBrush GetThemeColorBrush(Orc.Theming.ThemeColorStyle colorStyle = 0) { }
        public virtual void SynchronizeTheme() { }
    }
    public class ThemeService : Orc.Theming.IThemeService
    {
        public ThemeService(Orc.Theming.IAccentColorService accentColorService, Orc.Theming.IBaseColorSchemeService baseColorSchemeService) { }
        public virtual Orc.Theming.ThemeInfo GetThemeInfo() { }
        public virtual bool ShouldCreateStyleForwarders() { }
    }
}