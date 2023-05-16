namespace Core.Interfaces
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string casheKey, string response, TimeSpan timeToLive);
        Task<string> GetCachedResponseAsync(string casheKey);
    }
}
