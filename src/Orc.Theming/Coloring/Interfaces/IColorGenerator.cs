namespace Orc.Theming.Coloring
{
#if NETFX_CORE
    using Windows.UI;
#else
    using System.Windows.Media;

#endif

    /// <summary>
    ///     Basic interface for string to color generator.
    /// </summary>
    public interface IColorGenerator
    {
        #region Methods
        /// <summary>
        ///     Generates the color from string by hashing it.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="salt">The salt.</param>
        /// <returns>The color.</returns>
        Color Generate(object value, string salt = null);
        #endregion
    }
}
