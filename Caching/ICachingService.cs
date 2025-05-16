namespace Redis.Practice.Api.Caching
{
    public interface ICachingService
    {
        Task SetCache(string key, string value);
        Task<string?> GetCache(string key);
    }
}
