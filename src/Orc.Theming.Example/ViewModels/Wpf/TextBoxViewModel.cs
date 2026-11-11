namespace Orc.Theming.Example.ViewModels;

using System;
using System.ComponentModel.DataAnnotations;
using Catel.MVVM;

public class TextBoxViewModel : FeaturedViewModelBase
{
    public TextBoxViewModel(IServiceProvider serviceProvider) 
        : base(serviceProvider)
    {
        DeferValidationUntilFirstSaveCall = false;

        Validate(true);
    }

    public override string Title => "TextBox";

    [Required(ErrorMessage = "Text is required")]
    public string? ValidationTestText { get; set; }
}
