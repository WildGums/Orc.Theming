namespace Orc.Theming.Example.ViewModels
{
    using Catel.MVVM;
    using System.Threading.Tasks;
    using System.Timers;

    public class ProgressBarViewModel : ViewModelBase
    {
#pragma warning disable IDISP006 // Implement IDisposable
        private readonly Timer _timer = new Timer();
#pragma warning restore IDISP006 // Implement IDisposable

        public ProgressBarViewModel(/* dependency injection here */)
        {
        }

        public override string Title { get { return "View model title"; } }

        public int Value { get; set; } = 0;

        // TODO: Register models with the vmpropmodel codesnippet
        // TODO: Register view model properties with the vmprop or vmpropviewmodeltomodel codesnippets
        // TODO: Register commands with the vmcommand or vmcommandwithcanexecute codesnippets

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            _timer.Interval = 500;
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
            // TODO: subscribe to events here
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Value += 5;
            if (Value == 100)
            {
                _timer.Stop();
            }
        }

        protected override async Task CloseAsync()
        {
            // TODO: unsubscribe from events here

            await base.CloseAsync();
        }
    }
}
