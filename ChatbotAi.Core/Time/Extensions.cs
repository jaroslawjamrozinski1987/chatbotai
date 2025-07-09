using Microsoft.Extensions.DependencyInjection;

namespace ChatbotAi.Core.Time;

internal static class Extensions
{
    public static IServiceCollection AddTimeProvider(this IServiceCollection services)
    {
        services.AddSingleton<TimeProvider>(TimeProvider.System);
        return services;
    }
}
