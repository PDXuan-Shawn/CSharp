using System;
using System.Collections.Generic;

public sealed class CacheManager
{
    private static readonly Lazy<CacheManager> instance =
        new Lazy<CacheManager>(() => new CacheManager());

    private Dictionary<string, object> cacheStore;

    public static CacheManager Instance => instance.Value;

    private CacheManager()
    {
        cacheStore = new Dictionary<string, object>();
    }

    public void AddToCache(string key, object value)
    {
        if(!cacheStore.ContainsKey(key))
        {
            cacheStore[key] = value;
        }
    }

    public object GetFromCache(string key)
    {
        cacheStore.TryGetValue(key, out var value);
        return value;
    }

    public void RemoveFromCache(string key)
    {
        if (cacheStore.ContainsKey(key))
        {
            cacheStore.Remove(key);
        }
    }
}

class Program
{
    static void Main()
    {
        CacheManager cache = CacheManager.Instance;

        cache.AddToCache("UserId_12345", "John Doe");
        string userName = cache.GetFromCache("UserId_12345") as string;

        Console.WriteLine($"Cached User Name: {userName}");
    }
}
