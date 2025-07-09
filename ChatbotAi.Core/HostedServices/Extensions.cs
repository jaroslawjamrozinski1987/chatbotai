using Microsoft.Extensions.DependencyInjection;

namespace ChatbotAi.Core.HostedServices;

public static class Extensions
{
    public static IServiceCollection AddHostedServices(this IServiceCollection services)
        =>services.AddHostedService<ChatHostedService>();
}
