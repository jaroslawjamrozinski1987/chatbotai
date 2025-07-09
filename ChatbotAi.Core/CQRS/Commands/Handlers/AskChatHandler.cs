using ChatbotAi.Core.Consts;
using ChatbotAi.Core.Services.Abstractions;
using MediatR;

namespace ChatbotAi.Core.CQRS.Commands.Handlers;

internal class AskChatHandler(IChatService chatService, IItemStorage itemStorage) : INotificationHandler<AskChatCommand>
{
    public async Task Handle(AskChatCommand request, CancellationToken cancellationToken)
    {
        var result = await chatService.AddUserMessageAsync(request.userInput);
        itemStorage.Set<Guid>(ResultConsts.ResultKey, result);
    }
}
