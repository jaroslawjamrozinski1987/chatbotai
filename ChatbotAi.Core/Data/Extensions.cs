using ChatbotAi.Core.Data.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ChatbotAi.Core.Data;

internal static class Extensions
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(options => configuration.GetSection(DatabaseOptions.SectionName).Bind(options));
        services.AddDbContext<ChatDbContext>((serviceProvider, options) =>
        {
            var dbOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

            switch (dbOptions.Type.ToLowerInvariant())
            {
                case "sqlite":
                    options.UseSqlite(dbOptions.ConnectionString, x =>{
                        // x.MigrationsHistoryTable("MigrationsHistory")
                    });
                    break;
                case "sqlserver":
                    options.UseSqlServer(dbOptions.ConnectionString,x => x.MigrationsHistoryTable("MigrationsHistory"));
                    break;
                default:
                    throw new NotSupportedException($"Database type '{dbOptions.Type}' is not supported.");
            }
        });
        return services;
    }
}
