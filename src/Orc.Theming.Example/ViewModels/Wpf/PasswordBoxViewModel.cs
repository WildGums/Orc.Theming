namespace Orc.Theming.Example.ViewModels;

using System;
using System.ComponentModel.DataAnnotations;
using Catel.MVVM;

public class PasswordBoxViewModel : ViewModelBase
{
    public PasswordBoxViewModel(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }

    public override string Title => "PasswordBox";

    [Required(ErrorMessage = "Password is required")]
    public string? ValidationTestPassword { get; set; }
}
