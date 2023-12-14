namespace Orc.Theming;

using System;
using System.Collections;
using System.Linq;
using System.Resources;
using Catel;
using Catel.Logging;

public class ResourceDictionaryService : IResourceDictionaryService
{
    private static readonly ILog Log = LogManager.GetCurrentClassLogger();

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
                    Log.Debug($"Could not find generated resources @ '{generatedResourceName}', assuming the resource dictionary '{resourceDictionaryUri}' does not exist");
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
                    Log.Debug($"Resource '{resourceDictionaryUri}' exists");
                    return true;
                }
            }
        }

        Log.Debug($"Failed to confirm that resource '{resourceDictionaryUri}' exists");

        return false;
    }
}
