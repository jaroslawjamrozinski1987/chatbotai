using Microsoft.Extensions.DependencyInjection;

namespace ChatbotAi.Core.Dto;

internal static class Extensions
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(a =>
        {
            a.AddMaps(typeof(ChatMessageDto).Assembly);
        });
        return services;
    }
}
