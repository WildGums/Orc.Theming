namespace Orc.Theming.Tests.Services;

using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

[TestFixture]
public class ResourceDictionaryServiceFacts
{
    [TestCase("/Orc.Theming;component/themes/nonexisting.xaml", ExpectedResult = false)]
    [TestCase("/Orc.Theming;component/themes/generic.xaml", ExpectedResult = true)]
    public bool IsResourceDictionaryAvailable(string uri)
    {
        var resourceDictionaryService = new ResourceDictionaryService(NullLogger<ResourceDictionaryService>.Instance);
        return resourceDictionaryService.IsResourceDictionaryAvailable(uri);
    }
}
