namespace Orc.Theming;

public class BaseColorScheme
{
    public BaseColorScheme()
    {
        Name = string.Empty;
        ImageUri = string.Empty;
    }

    public string Name { get; set; }

    public string ImageUri { get; set; }
}