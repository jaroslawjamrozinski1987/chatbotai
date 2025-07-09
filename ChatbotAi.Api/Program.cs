
using ChatbotAi.Api.Endpoints;
using ChatbotAi.Api.Middleware;
using ChatbotAi.Core;
using ChatbotAi.Core.Data;

namespace ChatbotAi.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<ErrorHandlingMiddleware>();
        builder.Services.AddCore(builder.Configuration);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseApiEndpoints();
        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.UseMiddleware<ErrorHandlingMiddleware>(Array.Empty<object>());
        await app.Services.CreateDbAsync();
        await app.RunAsync();
    }
}
