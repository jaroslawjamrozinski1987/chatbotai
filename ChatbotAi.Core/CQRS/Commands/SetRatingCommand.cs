using MediatR;

namespace ChatbotAi.Core.CQRS.Commands;

public record SetRatingCommand(Guid messageId,byte rating) : INotification;
