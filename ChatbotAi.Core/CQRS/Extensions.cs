using ChatbotAi.Core.Data;
using Microsoft.Extensions.DependencyInjection;

namespace ChatbotAi.Core.CQRS;

internal static class Extensions
{
    public static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ChatDbContext).Assembly));
        return services;
    }
}
