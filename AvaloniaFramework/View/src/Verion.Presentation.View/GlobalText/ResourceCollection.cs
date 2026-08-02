using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;

namespace AvaloniaFramework.Presentation.View.GlobalText;

public class ResourceCollection
{
    private static readonly Dictionary<string, ResourceManager> FallbackResources
        = new Dictionary<string, ResourceManager>
        {
            { typeof(Dialog).FullName, Dialog.ResourceManager },
            { typeof(GroupName).FullName, GroupName.ResourceManager },
        };

    private readonly Dictionary<string, ResourceManager> resourceManagers;

    public ResourceCollection()
        : this(0)
    {
    }

    public ResourceCollection(int capacity)
    {
        resourceManagers = new Dictionary<string, ResourceManager>(
            capacity, StringComparer.Ordinal);
    }

    public void Add(Type resourceType)
    {
        if (resourceType == null)
            throw new ArgumentNullException(nameof(resourceType));

        string baseName = resourceType.FullName;
        if (resourceManagers.ContainsKey(baseName))
            throw new ArgumentException($"The resource '{baseName}' is already added", nameof(resourceType));

        var resManager = new ResourceManager(resourceType);
        resManager.GetString("PreloadResource", CultureInfo.CurrentCulture);
        resourceManagers.Add(baseName, resManager);
    }

    public string GetString(Type resourceType, string resourceName)
    {
        if (resourceType == null)
            throw new ArgumentNullException(nameof(resourceType));
        if (resourceName == null)
            throw new ArgumentNullException(nameof(resourceName));

        var currentCulture = CultureInfo.CurrentUICulture;
        string baseName = resourceType.FullName;
        if (FallbackResources.TryGetValue(baseName, out var resManager))
            return resManager.GetString(resourceName, currentCulture);

        // Automatically adds resource to the collection if its not already
        if (!resourceManagers.TryGetValue(baseName, out resManager))
        {
            resManager = new ResourceManager(resourceType);
            resourceManagers.Add(baseName, resManager);
        }

        string value = resManager.GetString(resourceName, CultureInfo.CurrentUICulture);

        // Fall-back to base resource managers
        if (value == null)
            ScanGetString(FallbackResources.Values, resourceName);

        return value;
    }

    public string GetString(string baseName, string resourceName)
    {
        if (baseName == null)
            throw new ArgumentNullException(nameof(baseName));
        if (resourceName == null)
            throw new ArgumentNullException(nameof(resourceName));

        var currentCulture = CultureInfo.CurrentUICulture;
        if (FallbackResources.TryGetValue(baseName, out var resManager))
            return resManager.GetString(resourceName, currentCulture);

        resManager = resourceManagers[baseName];
        string value = resManager.GetString(resourceName, currentCulture);

        // Fall-back to base resource managers
        if (value == null)
            ScanGetString(FallbackResources.Values, resourceName);

        return value;
    }

    public string GetString(string resourceName)
        => ScanGetString(resourceManagers.Values.Concat(FallbackResources.Values), resourceName);

    // Gets a localized string iterating through provided enumerator until a match is found
    private static string ScanGetString(IEnumerable<ResourceManager> colResource, string resourceName)
    {
        var currentCulture = CultureInfo.CurrentUICulture;
        foreach (var res in colResource)
        {
            string value = res.GetString(resourceName, currentCulture);
            if (value != null)
                return value;
        }

        return null;
    }
}