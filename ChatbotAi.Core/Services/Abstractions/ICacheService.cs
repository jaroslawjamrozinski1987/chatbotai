using ChatbotAi.Core.Types;

namespace ChatbotAi.Core.Services.Abstractions;

public interface ICacheService
{
    Task SetValue<T>(string key, T value, TimeSpan expiryTime);
    Task<CacheResponse<T>> GetValue<T>(string key, T defaultValue);
}
