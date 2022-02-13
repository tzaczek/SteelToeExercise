using System.Threading.Tasks;

namespace GitHub.Repository_Analyzer.Api.Cache
{
  public interface ICacheStorage
  {
    Task<T> GetCacheItem<T>(string key);
    Task SetCacheItem(string key, object value);
    Task RemoveCacheItem(string key);
  }
}