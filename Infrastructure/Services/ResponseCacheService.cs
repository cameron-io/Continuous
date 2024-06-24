using System.Text.Json;
using Core.Services;
using StackExchange.Redis;
using Infrastructure.Config;

namespace Infrastructure.Services;

public class ResponseCacheService(IConnectionMultiplexer redis) : IResponseCacheService
{
    private readonly IDatabase _database = redis.GetDatabase();

    public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
    {
        if (response == null) return;

        var serialisedResponse = JsonSerializer.Serialize(response, JsonSerializerOptionsProvider.GetOptions());

        await _database.StringSetAsync(cacheKey, serialisedResponse, timeToLive);
    }

    public async Task<string> GetCachedResponse(string cacheKey)
    {
        var cachedResponse = await _database.StringGetAsync(cacheKey);

        if (cachedResponse.IsNullOrEmpty) return null;

        return cachedResponse;
    }
}