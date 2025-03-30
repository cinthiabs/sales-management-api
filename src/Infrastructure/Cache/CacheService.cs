
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Infrastructure.Cache;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;
    private readonly DistributedCacheEntryOptions _options;
    private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };


    public CacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
        _options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
            SlidingExpiration = TimeSpan.FromSeconds(3600),
        };
    }
    public async Task<T?> GetAsync<T>(string key)
    {
        var cachedData = await _distributedCache.GetStringAsync(key);
        return cachedData != null ? JsonSerializer.Deserialize<T>(cachedData, _jsonOptions) : default;
    }

    public async Task SetAsync<T>(string key, T value)
    {
        var jsonData = JsonSerializer.Serialize(value);
        await _distributedCache.SetStringAsync(key, jsonData, _options);
    }
}
