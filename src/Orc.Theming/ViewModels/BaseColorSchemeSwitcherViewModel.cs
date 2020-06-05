namespace Orc.Theming.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catel;
    using Catel.MVVM;

    public class BaseColorSchemeSwitcherViewModel : ViewModelBase
    {
        private readonly IBaseColorSchemeService _baseColorSchemeService;

        public BaseColorSchemeSwitcherViewModel(ThemeManager themeManager, IBaseColorSchemeService baseColorSchemeService)
        {
            Argument.IsNotNull(() => baseColorSchemeService);

            _baseColorSchemeService = baseColorSchemeService;

            BaseColorSchemes = _baseColorSchemeService.GetAvailableBaseColorSchemes();
            SelectedBaseColorScheme = _baseColorSchemeService.GetBaseColorScheme() ?? BaseColorSchemes[0];
        }

        public IReadOnlyList<string> BaseColorSchemes { get; private set; }

        public string SelectedBaseColorScheme { get; set; }

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
            _baseColorSchemeService.SetBaseColorScheme(SelectedBaseColorScheme);
        }
    }
}
