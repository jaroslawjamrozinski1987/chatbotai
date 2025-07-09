using ChatbotAi.Core.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ChatbotAi.Core.Services;

internal static class Extensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IItemStorage, HttpContextItemStorage>();
        services.AddScoped<ICacheService, MemoryCacheService>();
        services.AddSingleton<IChatReplyQueue, ChatReplyQueue>();
        return services;
    }
}
