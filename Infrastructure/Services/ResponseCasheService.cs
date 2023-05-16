using Core.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class ResponseCasheService : IResponseCacheService
    {
        private readonly IDatabase _database;

        public ResponseCasheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task CacheResponseAsync(string casheKey, string response, TimeSpan timeToLive)
        {
            if (string.IsNullOrEmpty(response))
            {
                return;
            }
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var serializedResponse = JsonSerializer.Serialize(response, options);
            await _database.StringSetAsync(casheKey, serializedResponse);

        }

        public async Task<string> GetCachedResponseAsync(string casheKey)
        {
            var cashedResponse = await _database.StringGetAsync(casheKey);
            if (String.IsNullOrEmpty(cashedResponse))
            {
                return null;
            }
            return cashedResponse;
        }
    }
}
