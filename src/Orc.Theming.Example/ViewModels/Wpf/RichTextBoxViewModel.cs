namespace Orc.Theming.Example.ViewModels;

using System;
using System.ComponentModel.DataAnnotations;
using Catel.MVVM;

public class RichTextBoxViewModel : FeaturedViewModelBase
{
    public RichTextBoxViewModel(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        DeferValidationUntilFirstSaveCall = false;

        Validate(true);
    }

    public override string Title => "RichTextBox";

    [Required(ErrorMessage = "Content is required")]
    public string? ValidationTestContent { get; set; }
}
