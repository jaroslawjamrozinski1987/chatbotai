using ChatbotAi.Core.Services.Abstractions;
using ChatbotAi.Core.Types;
using Microsoft.Extensions.Caching.Memory;

namespace ChatbotAi.Core.Services;

internal class MemoryCacheService(IMemoryCache memoryCache) : ICacheService
{
    public Task<CacheResponse<T>> GetValue<T>(string key, T defaultValue)
    {
        var value = memoryCache.Get(key);

        if(value is T t)
        {
            return Task.FromResult(new CacheResponse<T>(true, t));
        }
        return Task.FromResult(new CacheResponse<T>(false, defaultValue));
    }

    public Task SetValue<T>(string key, T value, TimeSpan expiryTime)
    {
        memoryCache.Set(key, value, new MemoryCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = expiryTime,
            SlidingExpiration = expiryTime
        });
        return Task.CompletedTask;
    }
}
