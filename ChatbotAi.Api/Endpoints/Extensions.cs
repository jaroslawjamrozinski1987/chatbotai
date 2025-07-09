using ChatbotAi.Core.Consts;
using ChatbotAi.Core.CQRS.Commands;
using ChatbotAi.Core.CQRS.Queries;
using ChatbotAi.Core.Services.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatbotAi.Api.Endpoints;

public static class Extensions
{
    public static void UseApiEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/chat/ask", async (
     [FromBody] AskChatCommand askChat,
     [FromServices] IMediator mediator,
     [FromServices] IItemStorage itemStorage,
     CancellationToken cancellationToken) =>
        {
            await mediator.Publish(askChat, cancellationToken);
            return Results.Ok(itemStorage.Get<Guid>(ResultConsts.ResultKey));
        });

        app.MapGet("/chat/response/{messageId}", async (
    [FromRoute] Guid messageId,
    [FromServices] IMediator mediator,
    CancellationToken cancellationToken) =>
        {
            return Results.Ok(await mediator.Send(new GetResponseQuery(messageId), cancellationToken));
        });

        app.MapPost("/chat/rate", async (
            [FromBody] SetRatingCommand setRatingCommand,
            [FromServices] IMediator mediator) =>
        {
            await mediator.Publish(setRatingCommand);
            return Results.Ok();
        });

        app.MapPost("/chat/history", async ([FromBody] GetHistoryQuery getHistoryQuery, [FromServices] IMediator mediator) =>
        {
            var history = await mediator.Send(getHistoryQuery);
            return Results.Ok(history);
        });
    }
}
