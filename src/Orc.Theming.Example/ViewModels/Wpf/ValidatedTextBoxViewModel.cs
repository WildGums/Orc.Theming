namespace Orc.Theming.Example.ViewModels;

using System;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Catel.MVVM;
using Catel.IoC;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Catel.Data;

/// <summary>
/// ViewModel for the ValidatedTextBoxView.
/// 
/// This ViewModel demonstrates the use of the CustomValidationAdornerBehavior
/// by providing three validation examples with different adorner styles.
/// </summary>
public class ValidatedTextBoxViewModel : FeaturedViewModelBase
{
    public ValidatedTextBoxViewModel(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        ValidateExample1Command = new Command(serviceProvider, OnValidateExample1);
        ValidateExample2Command = new Command(serviceProvider, OnValidateExample2);
        ValidateExample3Command = new Command(serviceProvider, OnValidateExample3);
        ValidateExample4Command = new Command(serviceProvider, OnValidateExample4);

        DeferValidationUntilFirstSaveCall = false;

        Validate(true);
    }

    public override string Title => "Custom Validation Adorner";

    protected override void ValidateFields(List<IFieldValidationResult> validationResults)
    {
        base.ValidateFields(validationResults);

        if (string.IsNullOrWhiteSpace(Example0Text))
        {
            validationResults.Add(FieldValidationResult.CreateError(nameof(Example0Text), "Please enter some text for Example 0"));
        }
    }

    #region Example 0
    public string? Example0Text { get; set; }
    #endregion

    #region Example 1: Numeric Validation

    public string? Example1Text { get; set; }

    public bool Example1HasError { get; set; }

    public string? Example1ValidationMessage { get; set; }

    public ICommand ValidateExample1Command { get; }

    private void OnValidateExample1()
    {
        if (string.IsNullOrWhiteSpace(Example1Text))
        {
            Example1HasError = true;
            Example1ValidationMessage = "Please enter a number";
            return;
        }

        if (!int.TryParse(Example1Text, out _))
        {
            Example1HasError = true;
            Example1ValidationMessage = "Please enter a valid integer";
            return;
        }

        Example1HasError = false;
        Example1ValidationMessage = "✓ Valid number";
    }

    #endregion

    #region Example 2: Email Validation

    public string? Example2Text { get; set; }

    public bool Example2HasError { get; set; }

    public string? Example2ValidationMessage { get; set; }

    public ICommand ValidateExample2Command { get; }

    private void OnValidateExample2()
    {
        if (string.IsNullOrWhiteSpace(Example2Text))
        {
            Example2HasError = true;
            Example2ValidationMessage = "Please enter an email address";
            return;
        }

        // Simple email validation regex
        var emailPattern = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
        if (!Regex.IsMatch(Example2Text, emailPattern))
        {
            Example2HasError = true;
            Example2ValidationMessage = "Please enter a valid email address";
            return;
        }

        Example2HasError = false;
        Example2ValidationMessage = "✓ Valid email";
    }

    #endregion

    #region Example 3: Length Validation

    public string? Example3Text { get; set; }

    public bool Example3HasError { get; set; }

    public string? Example3ValidationMessage { get; set; }

    public ICommand ValidateExample3Command { get; }

    private void OnValidateExample3()
    {
        if (string.IsNullOrWhiteSpace(Example3Text))
        {
            Example3HasError = true;
            Example3ValidationMessage = "Please enter some text";
            return;
        }

        if (Example3Text.Length < 5)
        {
            Example3HasError = true;
            Example3ValidationMessage = $"Text must be at least 5 characters (currently {Example3Text.Length})";
            return;
        }

        if (Example3Text.Length > 20)
        {
            Example3HasError = true;
            Example3ValidationMessage = $"Text must not exceed 20 characters (currently {Example3Text.Length})";
            return;
        }

        Example3HasError = false;
        Example3ValidationMessage = "✓ Valid length";
    }

    #endregion

    #region Example 4: Direct Behavior Example

    public string? Example4Text { get; set; }

    public bool Example4HasError { get; set; }

    public string? Example4ValidationMessage { get; set; }

    public ICommand ValidateExample4Command { get; }

    private void OnValidateExample4()
    {
        Example4ValidationMessage = null;
        Example4HasError = false;

        if (string.IsNullOrWhiteSpace(Example4Text))
        {
            Example4HasError = true;
            Example4ValidationMessage = "This field is required";
            return;
        }

        Example4HasError = false;
        Example4ValidationMessage = "✓ Valid";
    }

    #endregion
}
