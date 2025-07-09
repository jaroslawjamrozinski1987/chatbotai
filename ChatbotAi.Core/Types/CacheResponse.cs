namespace ChatbotAi.Core.Types;

public record CacheResponse<T>(bool hasValue, T value);