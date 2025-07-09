using ChatbotAi.Core.Services.Abstractions;
using MediatR;

namespace ChatbotAi.Core.CQRS.Commands.Handlers;

internal class SetRatingHandler(IChatService chatService) : INotificationHandler<SetRatingCommand>
{
    public Task Handle(SetRatingCommand request, CancellationToken cancellationToken)
    => chatService.SetRatingAsync(request.messageId, request.rating);
}
