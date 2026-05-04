namespace Orc.Theming;

using System;
using System.Collections;
using System.Linq;
using System.Resources;
using Catel;
using Catel.Logging;
using Microsoft.Extensions.Logging;

public class ResourceDictionaryService : IResourceDictionaryService
{
    private readonly ILogger<ResourceDictionaryService> _logger;

    public ResourceDictionaryService(ILogger<ResourceDictionaryService> logger)
    {
        _logger = logger;
    }

    /// <summary>
    ///     Checks whether the specified resource dictionary is available as resource.
    /// </summary>
    /// <param name="resourceDictionaryUri">The resource dictionary uri.</param>
    /// <returns></returns>
    public virtual bool IsResourceDictionaryAvailable(string resourceDictionaryUri)
    {
        var expectedResourceNames = resourceDictionaryUri.Split(new[] {";component/"}, StringSplitOptions.RemoveEmptyEntries);
        if (expectedResourceNames.Length == 2)
        {
            // Part 1 is assembly
#pragma warning disable CA1307 // Specify StringComparison
            var assemblyName = expectedResourceNames[0].Replace("/", string.Empty);
#pragma warning restore CA1307 // Specify StringComparison
            var assembly = (from x in AppDomain.CurrentDomain.GetAssemblies()
                where x.GetName().Name?.EqualsIgnoreCase(assemblyName) ?? false
                select x).FirstOrDefault();
            if (assembly is not null)
            {
                // Orchestra.Core.g.resources
                var generatedResourceName = $"{assembly.GetName().Name}.g.resources";

                using var resourceStream = assembly.GetManifestResourceStream(generatedResourceName);
                if (resourceStream is null)
                {
                    _logger.LogDebug("Could not find generated resources @ '{GeneratedResourceName}', assuming the resource dictionary '{ResourceDictionaryUri}' does not exist", generatedResourceName, resourceDictionaryUri);
                    return false;
                }

#pragma warning disable CA1307 // Specify StringComparison
                var relativeResourceName = expectedResourceNames[1].Replace(".xaml", ".baml");
#pragma warning restore CA1307 // Specify StringComparison

                using var reader = new ResourceReader(resourceStream);
                var exists = (from x in reader.Cast<DictionaryEntry>()
                    where ((string)x.Key).EqualsIgnoreCase(relativeResourceName)
                    select x).Any();
                if (exists)
                {
                    _logger.LogDebug("Resource '{ResourceDictionaryUri}' exists", resourceDictionaryUri);
                    return true;
                }
            }
        }

        _logger.LogDebug("Failed to confirm that resource '{ResourceDictionaryUri}' exists", resourceDictionaryUri);

        return false;
    }
}
