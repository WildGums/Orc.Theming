namespace Orc.Theming.Coloring;

using System.Windows.Media;

/// <summary>
///     Basic interface for string to color generator.
/// </summary>
public interface IColorGenerator
{
    /// <summary>
    ///     Generates the color from string by hashing it.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="salt">The salt.</param>
    /// <returns>The color.</returns>
    Color Generate(object value, string? salt = null);
}