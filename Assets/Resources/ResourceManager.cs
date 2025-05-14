using UnityEngine;

public static class ResourceManager
{
    public static ExpierencePickup ExpierencePickup => LoadResource<ExpierencePickup>("Exp");

    public static T LoadResource<T>(string path) where T : Object
    {
        var resource = Resources.Load<T>(path);
        if (resource == null)
        {
            Debug.LogError($"Resource not found at path: {path}");
            return null;
        }
        return resource;
    }
}
