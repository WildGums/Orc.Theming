namespace Orc.Theming.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Catel.IoC;
    using Catel.MVVM;

    public class BaseColorSchemeSwitcherWithIconViewModel : ViewModelBase
    {
        private readonly IBaseColorSchemeService _baseColorSchemeService;
        private readonly IServiceLocator _serviceLocator;

#pragma warning disable CA1801 // Review unused parameters
        public BaseColorSchemeSwitcherWithIconViewModel(IServiceLocator serviceLocator, IBaseColorSchemeService baseColorSchemeService)
#pragma warning restore CA1801 // Review unused parameters
        {
            ArgumentNullException.ThrowIfNull(serviceLocator);
            ArgumentNullException.ThrowIfNull(baseColorSchemeService);

            _serviceLocator = serviceLocator;
            _baseColorSchemeService = baseColorSchemeService;

            var baseColorSchemes = new List<BaseColorScheme>();

            foreach (var baseColorSchemeFromService in _baseColorSchemeService.GetAvailableBaseColorSchemes())
            {
                var baseColorScheme = new BaseColorScheme
                {
                    Name = baseColorSchemeFromService,
                    ImageUri = $"/Orc.Theming;component/Resources/Images/BaseColor_{baseColorSchemeFromService}.png"
                };

                baseColorSchemes.Add(baseColorScheme);
            }

            BaseColorSchemes = baseColorSchemes;

            var selected = _baseColorSchemeService.GetBaseColorScheme() ?? BaseColorSchemes[0].Name;

            SelectedBaseColorScheme = BaseColorSchemes.FirstOrDefault(x => x.Name == selected);
        }

        public IReadOnlyList<BaseColorScheme> BaseColorSchemes { get; }

        public BaseColorScheme? SelectedBaseColorScheme { get; set; }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            _baseColorSchemeService.BaseColorSchemeChanged += OnBaseColorSchemeServiceBaseColorSchemeChanged;
        }

        protected override async Task CloseAsync()
        {
            _baseColorSchemeService.BaseColorSchemeChanged -= OnBaseColorSchemeServiceBaseColorSchemeChanged;

            await base.CloseAsync();
        }

        private void OnBaseColorSchemeServiceBaseColorSchemeChanged(object? sender, EventArgs e)
        {
            var newlySelected = _baseColorSchemeService.GetBaseColorScheme();

            SelectedBaseColorScheme = BaseColorSchemes.FirstOrDefault(x => x.Name == newlySelected);
        }

        private async void OnSelectedBaseColorSchemeChanged()
        {
            var selected = SelectedBaseColorScheme;
            if (selected is null)
            {
                return;
            }

            // Note: short delay to allow some animation
            await Task.Delay(200);

            _baseColorSchemeService.SetBaseColorScheme(selected.Name);
        }
    }
}
