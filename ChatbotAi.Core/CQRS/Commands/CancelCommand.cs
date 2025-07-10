using MediatR;
namespace ChatbotAi.Core.CQRS.Commands;

public record CancelCommand : INotification;
