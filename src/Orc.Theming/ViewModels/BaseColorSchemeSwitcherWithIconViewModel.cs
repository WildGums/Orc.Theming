namespace Orc.Theming.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows;
    using Catel;
    using Catel.IoC;
    using Catel.MVVM;
    using Catel.MVVM.Views;
    using Orc.Theming.Views;

    public class BaseColorSchemeSwitcherWithIconViewModel : ViewModelBase
    {
        private readonly IBaseColorSchemeService _baseColorSchemeService;
        private readonly IServiceLocator _serviceLocator;

#pragma warning disable CA1801 // Review unused parameters
        public BaseColorSchemeSwitcherWithIconViewModel(IServiceLocator serviceLocator, IBaseColorSchemeService baseColorSchemeService)
#pragma warning restore CA1801 // Review unused parameters
        {
            Argument.IsNotNull(() => serviceLocator);
            Argument.IsNotNull(() => baseColorSchemeService);

            _serviceLocator = serviceLocator;
            _baseColorSchemeService = baseColorSchemeService;

            BaseColorSchemes = _baseColorSchemeService.GetAvailableBaseColorSchemes();
            BaseColorSchemeUris = _baseColorSchemeService.GetAvailableBaseColorSchemeUris();

            SelectedBaseColorScheme = _baseColorSchemeService.GetBaseColorScheme() ?? BaseColorSchemes[0];
            updateSelectedBaseColorUri();
        }

        public IReadOnlyList<string> BaseColorSchemes { get; }

        public IReadOnlyList<KeyValuePair<string, string>> BaseColorSchemeUris { get; }

        public string SelectedBaseColorScheme { get; set; }

        public KeyValuePair<string, string> SelectedBaseColorSchemeUri { get; set; }

        public Visibility ShowImages { get; set; }

        public Visibility ShowText { get; set; }

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

        private void OnBaseColorSchemeServiceBaseColorSchemeChanged(object sender, EventArgs e)
        {
            SelectedBaseColorScheme = _baseColorSchemeService.GetBaseColorScheme();
        }

        private void OnSelectedBaseColorSchemeChanged()
        {
            updateSelectedBaseColorUri();
            _baseColorSchemeService.SetBaseColorScheme(SelectedBaseColorScheme);
        }

        private void OnSelectedBaseColorSchemeUriChanged()
        {
            updateSelectedBaseColor();
            _baseColorSchemeService.SetBaseColorScheme(SelectedBaseColorScheme);
        }

        private void updateSelectedBaseColorUri()
        {
            for (var i = 0; i < BaseColorSchemes.Count; i++)
                if (SelectedBaseColorScheme == BaseColorSchemes[i])
                    SelectedBaseColorSchemeUri = BaseColorSchemeUris[i];
        }

        private void updateSelectedBaseColor()
        {
            for (var i = 0; i < BaseColorSchemeUris.Count; i++)
                if (SelectedBaseColorSchemeUri.Equals(BaseColorSchemeUris[i]))
                    SelectedBaseColorScheme = BaseColorSchemes[i];
        }
    }
}
