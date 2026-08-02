using System;
using System.ComponentModel.DataAnnotations;

namespace AvaloniaFramework.Presentation.View.GlobalText;

public static class DisplayAttributeInternationalizable
{
    private static readonly Lazy<ResourceCollection> SharedResCollection
        = new Lazy<ResourceCollection>(() => new ResourceCollection(), true);

    // TODO: Evaluate whether ResourceCollection instance should be static
    public static string GetLocalizedDisplayName(this DisplayAttribute displayAttrib)
        => GetString(displayAttrib, SharedResCollection.Value, displayAttrib?.Name);

    public static string GetLocalizedDisplayName(this DisplayAttribute displayAttrib, ResourceCollection resCollection)
        => GetString(displayAttrib, resCollection, displayAttrib?.Name);

    public static string GetLocalizedDisplayName(this ResourceCollection resCollection, DisplayAttribute displayAttrib)
        => GetString(displayAttrib, resCollection, displayAttrib?.Name);

    // TODO: Evaluate whether ResourceCollection instance should be static
    public static string GetLocalizedGroupName(this DisplayAttribute displayAttrib)
        => GetString(displayAttrib, SharedResCollection.Value, displayAttrib?.GroupName);

    public static string GetLocalizedGroupName(this DisplayAttribute displayAttrib, ResourceCollection resCollection)
        => GetString(displayAttrib, resCollection, displayAttrib?.GroupName);

    public static string GetLocalizedGroupName(this ResourceCollection resCollection, DisplayAttribute displayAttrib)
        => GetString(displayAttrib, resCollection, displayAttrib?.GroupName);

    // TODO: Evaluate whether ResourceCollection instance should be static
    public static string GetLocalizedDescription(this DisplayAttribute displayAttrib)
        => GetString(displayAttrib, SharedResCollection.Value, displayAttrib?.Description);

    public static string GetLocalizedDescription(this DisplayAttribute displayAttrib, ResourceCollection resCollection)
        => GetString(displayAttrib, resCollection, displayAttrib?.Description);

    public static string GetLocalizedDescription(this ResourceCollection resCollection, DisplayAttribute displayAttrib)
        => GetString(displayAttrib, resCollection, displayAttrib?.Description);

    private static string GetString(DisplayAttribute attrib, ResourceCollection resCollection, string resourceName)
    {
        if (attrib == null)
            throw new ArgumentNullException(nameof(attrib));
        if (resCollection == null)
            throw new ArgumentNullException(nameof(resCollection));

        if (resourceName == null)
            return null;
        if (attrib.ResourceType == null)
            return resourceName;

        string value = attrib.GetName();
        if (attrib.ResourceType != null)
            value = resCollection.GetString(attrib.ResourceType, resourceName);

        return value ?? resourceName;
    }
}