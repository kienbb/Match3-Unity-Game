using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager
{
    private static Dictionary<Type, object> GameObjects_cache = new Dictionary<Type, object>();

    private class ResourcesByType<T> : Dictionary<string, T> where T : UnityEngine.Object { }

    internal static T Load<T>(string sourceName) where T : UnityEngine.Object
    {
        Type type = typeof(T);
        if (!GameObjects_cache.ContainsKey(type))
        {
            GameObjects_cache.Add(type, new ResourcesByType<T>());
        }

        ResourcesByType<T> resourcesByType = (ResourcesByType<T>)GameObjects_cache[type];

        if (!resourcesByType.ContainsKey(sourceName))
        {
            T resource = Resources.Load<T>(sourceName);
            if (resource == null)
            {
                Debug.LogError("Resource not found: " + sourceName);
            }
            resourcesByType.Add(sourceName, resource);
        }

        return resourcesByType[sourceName];
    }
}
