namespace Orc.Theming
{
    public interface IThemeService
    {
        bool ShouldCreateStyleForwarders();
        ThemeInfo GetThemeInfo();
    }
}
