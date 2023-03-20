﻿namespace Orc.Theming;

public enum ThemeColorStyle
{
    AccentColor = 0,
    AccentColor80 = 1,
    AccentColor60 = 2,
    AccentColor40 = 3,
    AccentColor20 = 4,

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

    // Default aliases shared for all controls, these should be similar to the aliases in Themes/Theme.Template.xaml
    DefaultBorder = BorderColor,
    DefaultBackground = BackgroundColor,
    DefaultForeground = ForegroundColor,
    DefaultText = Text,

    CheckedBorder = HighlightColor,
    CheckedBackground = AccentColor20,
    CheckedForeground = ForegroundColor,
    CheckedText = Text,

    CheckedMouseOverBorder = AccentColor60,
    CheckedMouseOverBackground = AccentColor20,
    CheckedMouseOverForeground = ForegroundColor,
    CheckedMouseOverText = Text,

    MouseOverBorder = AccentColor40,
    MouseOverBackground = AccentColor20,
    MouseOverForeground = ForegroundColor,
    MouseOverText = Text,

    PressedBorder = AccentColor60,
    PressedBackground = AccentColor40,
    PressedForeground = ForegroundColor,
    PressedText = Text,

    DisabledBorder = Gray5,
    DisabledBackground = AccentColor40,
    DisabledForeground = ForegroundColor,
    DisabledText = Text,

    HighlightedBorder = AccentColor60,
    HighlightedBackground = AccentColor40,
    HighlightedForeground = ForegroundColor,
    HighlightedText = Text,

    SelectionActiveBorder = AccentColor80,
    SelectionActiveBackground = AccentColor60,
    SelectionActiveForeground = ForegroundColor,
    SelectionActiveText = Text,

    SelectionInactiveBorder = AccentColor40,
    SelectionInactiveBackground = AccentColor20,
    SelectionInactiveForeground = ForegroundColor,
    SelectionInactiveText = Text,
}