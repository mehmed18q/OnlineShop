using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Utilities
{
    public class CacheUtility(IMemoryCache memoryCache)
    {
        private readonly IMemoryCache _memoryCache = memoryCache;

        public void SetMemoryCache(string cacheKey, object value, TimeSpan expireTime)
        {
            MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(expireTime);

            _ = _memoryCache.Set(cacheKey, value, cacheEntryOptions);
        }

        public void RemoveMemoryCache(string cacheKey)
        {
            _memoryCache.Remove(cacheKey);
        }

        public bool TryGetValue<TResult>(string cacheKey, out TResult? value)
        {
            return _memoryCache.TryGetValue(cacheKey, out value);
        }
    }
}
