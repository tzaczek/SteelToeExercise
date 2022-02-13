using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GitHub.Repository_Analyzer.Api.Cache
{
  public class RedisCacheStorage : ICacheStorage
  {
    private readonly ILogger<RedisCacheStorage> _logger;
    private readonly IDistributedCache _cache;

    public RedisCacheStorage(ILogger<RedisCacheStorage> logger, IDistributedCache cache)
    {
      _logger = logger;
      _cache = cache;
    }

    public async Task<T> GetCacheItem<T>(string key)
    {
      _logger.LogDebug($"Get item from {nameof(RedisCacheStorage)}");

      var cacheItem = await _cache.GetStringAsync(key);

      return string.IsNullOrEmpty(cacheItem)
        ? default
        : JsonConvert.DeserializeObject<T>(cacheItem);
    }

    public Task SetCacheItem(string key, object value)
    {
      _logger.LogDebug($"Set item from {nameof(RedisCacheStorage)}");

      var cacheItem = JsonConvert.SerializeObject(value);

      return _cache.SetStringAsync(key, cacheItem);
    }

    public Task RemoveCacheItem(string key)
    {
      _logger.LogDebug($"Remove item from {nameof(RedisCacheStorage)}");
      
      return _cache.RemoveAsync(key);
    }
  }
}
