using ChatbotAi.Core.Services.Abstractions;
using MediatR;

namespace ChatbotAi.Core.CQRS.Commands.Handlers;

internal class CancelHandler(IChatReplyQueue replyQueue) : INotificationHandler<CancelCommand>
{
    public Task Handle(CancelCommand notification, CancellationToken cancellationToken)
        => replyQueue.Clear();
}
