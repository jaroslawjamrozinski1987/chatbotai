using ChatbotAi.Core.Services.Abstractions;
using ChatbotAi.Core.Types;
using MediatR;

namespace ChatbotAi.Core.CQRS.Queries.Handlers;

internal class GetResponseHandler(IChatService chatService, ICacheService cacheService) : IRequestHandler<GetResponseQuery, ChatResponse>
{
    public async Task<ChatResponse> Handle(GetResponseQuery request, CancellationToken cancellationToken)
    {
        var userMessageId = request.messageId;
        var key = $"bot_response_{userMessageId}";

        var responseFromCache = await cacheService.GetValue<int>(key, 0);
        var canContinue = true;
        if (!responseFromCache.hasValue)
        {
            var rndValue = new Random().Next(0, 3);
            await cacheService.SetValue(key, rndValue, TimeSpan.FromHours(10));
            canContinue = rndValue == 0;
        }
        //symulacja czy można odpowiedzieć na pytanie
        else if (responseFromCache.value > 0)
        {
            await cacheService.SetValue(key, responseFromCache.value - 1, TimeSpan.FromHours(10));
            canContinue = false;
        }

        if (!canContinue)
        {
            return new ChatResponse(false, null);
        }
        return await chatService.GetResponse(request.messageId);
    }
}
