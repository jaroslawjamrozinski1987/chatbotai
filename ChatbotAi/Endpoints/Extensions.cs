using System.Security;

namespace ChatbotAi.Endpoints;

public static class Extensions
{
    public static void UseChatEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/chat/me", async (
            CancellationToken cancellationToken = default) =>
        {
            return Results.NotFound(new { Message = "Endpoint is disabled" });


        });
    }
}
