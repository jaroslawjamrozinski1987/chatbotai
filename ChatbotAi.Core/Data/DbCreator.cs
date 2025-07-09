using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace ChatbotAi.Core.Data;

public static class DbCreator
{
    public static async Task CreateDbAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<ChatDbContext>();
        //await db.Database.EnsureCreatedAsync();
        await db.Database.MigrateAsync(CancellationToken.None);
    }
}
