using Microsoft.Extensions.Caching.Distributed;

namespace Redis.Practice.Api.Caching
{
    public class CachingService : ICachingService
    {
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _options;

        public CachingService(IDistributedCache cache)
        {
            _cache = cache;
            _options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1000),
                SlidingExpiration = TimeSpan.FromSeconds(1000)
            };
        }

        public async Task<string?> GetCache(string key)
        {
            return await _cache.GetStringAsync(key);
        }

        public async Task SetCache(string key, string value)
        {
            await _cache.SetStringAsync(key, value, _options);
        }
    }
}
