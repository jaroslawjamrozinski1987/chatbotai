using ChatbotAi.Core.CQRS;
using ChatbotAi.Core.Data;
using ChatbotAi.Core.Dto;
using ChatbotAi.Core.HostedServices;
using ChatbotAi.Core.Services;
using ChatbotAi.Core.Time;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatbotAi.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddTimeProvider();
        services.AddServices();
        services.AddMapper();
        services.AddMemoryCache();
        services.AddMediatR();
        services.AddHostedServices();
        return services;
    }
}
