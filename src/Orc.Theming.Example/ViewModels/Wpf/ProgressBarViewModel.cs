namespace Orc.Theming.Example.ViewModels;

using System;
using System.Threading.Tasks;
using System.Timers;
using Catel.MVVM;

public class ProgressBarViewModel : ViewModelBase
{
#pragma warning disable IDISP006 // Implement IDisposable
    private readonly Timer _timer = new();
#pragma warning restore IDISP006 // Implement IDisposable

    public ProgressBarViewModel(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }

    public override string Title { get { return "ProgressBar"; } }

    public int Value { get; set; } = 0;

    protected override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        _timer.Interval = 500;
        _timer.Elapsed += OnTimerElapsed;
        _timer.Start();
    }

    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        Value += 5;
        if (Value == 100)
        {
            _timer.Stop();
        }
    }
}
