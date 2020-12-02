// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorHSB.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Theming.Coloring
{
    using System;

    /// <summary>
    /// Color in HSB model.
    /// </summary>
    /// <summary>
    /// Structure to define HSB.
    /// </summary>
    public struct ColorHsb
    {
        #region Constants
        /// <summary>
        /// Gets an empty HSB structure.
        /// </summary>
        public static readonly ColorHsb Empty = new ColorHsb();
        #endregion

        #region Fields
        private double _brightness;
        private double _hue;
        private double _saturation;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an instance of a HSB structure.
        /// </summary>
        /// <param name="h">Hue value.</param>
        /// <param name="s">Saturation value.</param>
        /// <param name="b">Brightness value.</param>
        public ColorHsb(double h, double s, double b)
        {
            if (h > 360)
            {
                _hue = 360;
            }
            else
            {
                _hue = h >= 0 ? h : 0;
            }

            if (s > 1)
            {
                _saturation = 1;
            }
            else
            {
                _saturation = s >= 0 ? s : 0;
            }

            if (b > 1)
            {
                _brightness = 1;
            }
            else
            {
                _brightness = b >= 0 ? b : 0;
            }

        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the brightness component.
        /// </summary>
        public double Brightness
        {
            get { return _brightness; }
            set
            {
                if (value > 1)
                {
                    _brightness = 1;
                }
                else
                {
                    _brightness = value >= 0 ? value : 0;
                }
            }
        }

        /// <summary>
        /// Gets or sets the hue component.
        /// </summary>
        public double Hue
        {
            get { return _hue; }
            set
            {
                if (value > 360)
                {
                    _hue = 360;
                }
                else
                {
                    _hue = value >= 0 ? value : 0;
                }
            }
        }

        /// <summary>
        /// Gets or sets saturation component.
        /// </summary>
        public double Saturation
        {
            get { return _saturation; }
            set
            {
                if (value > 1)
                {
                    _saturation = 1;
                }
                else
                {
                    _saturation = value >= 0 ? value : 0;
                }
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Checks inequality of two Colors in HSB.
        /// </summary>
        /// <param name="item1">First color.</param>
        /// <param name="item2">Second color.</param>
        /// <returns></returns>
        public static bool operator !=(ColorHsb item1, ColorHsb item2)
        {
            return item1.Hue != item2.Hue
                   || item1.Saturation != item2.Saturation
                   || item1.Brightness != item2.Brightness;
        }

        /// <summary>
        /// Checks equality of two Colors in HSB.
        /// </summary>
        /// <param name="item1">First color.</param>
        /// <param name="item2">Second color.</param>
        /// <returns></returns>
        public static bool operator ==(ColorHsb item1, ColorHsb item2)
        {
            return item1.Hue == item2.Hue
                   && item1.Saturation == item2.Saturation
                   && item1.Brightness == item2.Brightness;
        }

        /// <summary>
        /// Checks of equality of two colors in HSB.
        /// </summary>
        /// <param name="obj">Second color object.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is null || GetType() != obj.GetType())
            {
                return false;
            }

            return (this == (ColorHsb) obj);
        }

        /// <summary>
        /// Calculates Hash code for this color.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Hue.GetHashCode() ^ Saturation.GetHashCode() ^
                   Brightness.GetHashCode();
        }
        #endregion
    }
}
