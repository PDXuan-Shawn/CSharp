
using Microsoft.Extensions.Caching.Memory;

public interface ICacheService
{
    void Set<T>(string key, T value, TimeSpan expiration);
    T Get<T>(string key);
    void Remove(string key);
}


public class InMemoryCacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;

    public InMemoryCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public void Set<T>(string key, T value, TimeSpan expiration)
    {
        _memoryCache.Set(key, value, expiration);
    }

    public T Get<T>(string key)
    {
        _memoryCache.TryGetValue(key, out T value);
        return value;
    }

    public void Remove(string key)
    {
        _memoryCache.Remove(key);
    }
}



using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

public class RedisCacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;

    public RedisCacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public void Set<T>(string key, T value, TimeSpan expiration)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        };
        var jsonData = JsonSerializer.Serialize(value);
        _distributedCache.SetString(key, jsonData, options);
    }

    public T Get<T>(string key)
    {
        var jsonData = _distributedCache.GetString(key);
        return jsonData == null ? default(T) : JsonSerializer.Deserialize<T>(jsonData);
    }

    public void Remove(string key)
    {
        _distributedCache.Remove(key);
    }
}




using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class CacheServiceFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;

    public CacheServiceFactory(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        _configuration = configuration;
    }

    public ICacheService CreateCacheService()
    {
        var cacheType = _configuration.GetValue<string>("CacheType");
        return cacheType switch
        {
            "InMemory" => _serviceProvider.GetService<InMemoryCacheService>(),
            "Redis" => _serviceProvider.GetService<RedisCacheService>(),
            _ => throw new InvalidOperationException("Unsupported cache type")
        };
    }
}



public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = "localhost:6379"; // Your Redis configuration
        });

        services.AddSingleton<InMemoryCacheService>();
        services.AddSingleton<RedisCacheService>();
        services.AddSingleton<CacheServiceFactory>();

        // Add other services
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the middleware pipeline
    }
}



using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class CacheController : ControllerBase
{
    private readonly CacheServiceFactory _cacheServiceFactory;

    public CacheController(CacheServiceFactory cacheServiceFactory)
    {
        _cacheServiceFactory = cacheServiceFactory;
    }

    [HttpGet("{key}")]
    public IActionResult Get(string key)
    {
        var cacheService = _cacheServiceFactory.CreateCacheService();
        var value = cacheService.Get<string>(key);
        return Ok(value);
    }

    [HttpPost]
    public IActionResult Set(string key, string value)
    {
        var cacheService = _cacheServiceFactory.CreateCacheService();
        cacheService.Set(key, value, TimeSpan.FromMinutes(5));
        return Ok();
    }

    [HttpDelete("{key}")]
    public IActionResult Remove(string key)
    {
        var cacheService = _cacheServiceFactory.CreateCacheService();
        cacheService.Remove(key);
        return Ok();
    }
}
