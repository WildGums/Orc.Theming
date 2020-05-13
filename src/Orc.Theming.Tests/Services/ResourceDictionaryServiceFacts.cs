namespace Orc.Theming.Tests.Services
{
    using NUnit.Framework;

    [TestFixture]
    public class ResourceDictionaryServiceFacts
    {
        [TestCase("/Orc.Theming;component/themes/nonexisting.xaml", ExpectedResult = false)]
        [TestCase("/Orc.Theming;component/themes/generic.xaml", ExpectedResult = true)]
        public bool IsResourceDictionaryAvailable(string uri)
        {
            var resourceDictionaryService = new ResourceDictionaryService();
            return resourceDictionaryService.IsResourceDictionaryAvailable(uri);
        }
    }
}
