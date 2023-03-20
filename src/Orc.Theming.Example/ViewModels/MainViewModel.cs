namespace Orc.Theming.Example.ViewModels;

using Catel.MVVM;

public class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        DeferValidationUntilFirstSaveCall = false;
    }
    public override string Title => "Orc.Theming example";
}