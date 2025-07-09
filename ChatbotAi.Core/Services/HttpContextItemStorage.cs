using ChatbotAi.Core.Services.Abstractions;
using Microsoft.AspNetCore.Http;

namespace ChatbotAi.Core.Services;

internal sealed class HttpContextItemStorage(IHttpContextAccessor httpContextAccessor) : IItemStorage
{
    public void Set<T>(string key, T item)
    => httpContextAccessor.HttpContext?.Items.TryAdd(key, item);

    public T Get<T>(string key)
    {
        if (!(httpContextAccessor.HttpContext is not null && httpContextAccessor.HttpContext.Items.TryGetValue(key, out var item)))
        {
            return default;
        }

         return (T)item;
    }
}