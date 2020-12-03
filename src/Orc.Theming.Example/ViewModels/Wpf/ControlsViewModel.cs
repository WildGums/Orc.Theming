namespace Orc.Theming.Example.ViewModels
{
    using System.Collections.Generic;
    using Catel.Data;
    using Catel.MVVM;

    public class ControlsViewModel : ViewModelBase
    {
        public ControlsViewModel()
        {

        }

        public string Text { get; set; }

        protected override void ValidateFields(List<IFieldValidationResult> validationResults)
        {
            if (string.IsNullOrEmpty(Text))
            {
                validationResults.Add(new FieldValidationResult(nameof(Text), ValidationResultType.Error, "Text cannot be empty"));
                validationResults.Add(new FieldValidationResult(nameof(Text), ValidationResultType.Warning, "Text cannot be empty"));
            }

            base.ValidateFields(validationResults);
        }
    }
}
