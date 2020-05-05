namespace Orc.Theming
{
    public enum ThemeColorStyle
    {
        AccentColor = 0,
        AccentColor80 = 1,
        AccentColor60 = 2,
        AccentColor40 = 3,
        AccentColor20 = 4,

        // Backwards compatibility
        [ObsoleteEx(ReplacementTypeOrMember = "AccentColor80", TreatAsErrorFromVersion = "4.0", RemoveInVersion = "5.0")]
        AccentColor1 = AccentColor80,
        [ObsoleteEx(ReplacementTypeOrMember = "AccentColor60", TreatAsErrorFromVersion = "4.0", RemoveInVersion = "5.0")]
        AccentColor2 = AccentColor60,
        [ObsoleteEx(ReplacementTypeOrMember = "AccentColor40", TreatAsErrorFromVersion = "4.0", RemoveInVersion = "5.0")]
        AccentColor3 = AccentColor40,
        [ObsoleteEx(ReplacementTypeOrMember = "AccentColor20", TreatAsErrorFromVersion = "4.0", RemoveInVersion = "5.0")]
        AccentColor4 = AccentColor20,

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

        // Default aliases shared for all controls, these should be similar to the aliases in Themes/Theme.Template.xaml
        DefaultBorder = BorderColor,
        DefaultBackground = BackgroundColor,
        DefaultForeground = ForegroundColor,

        CheckedBorder = HighlightColor,
        CheckedBackground = AccentColor20,
        CheckedForeground = ForegroundColor,

        CheckedMouseOverBorder = AccentColor60,
        CheckedMouseOverBackground = AccentColor20,
        CheckedMouseOverForeground = ForegroundColor,

        MouseOverBorder = AccentColor40,
        MouseOverBackground = AccentColor20,
        MouseOverForeground = ForegroundColor,

        PressedBorder = AccentColor60,
        PressedBackground = AccentColor40,
        PressedForeground = ForegroundColor,

        DisabledBorder = Gray5,
        DisabledBackground = AccentColor40,
        DisabledForeground = ForegroundColor,

        HighlightedBorder = AccentColor60,
        HighlightedBackground = AccentColor40,
        HighlightedForeground = ForegroundColor,

        SelectionActiveBorder = AccentColor80,
        SelectionActiveBackground = AccentColor60,
        SelectionActiveForeground = ForegroundColor,

        SelectionInactiveBorder = AccentColor40,
        SelectionInactiveBackground = AccentColor20,
        SelectionInactiveForeground = ForegroundColor,
    }
}
