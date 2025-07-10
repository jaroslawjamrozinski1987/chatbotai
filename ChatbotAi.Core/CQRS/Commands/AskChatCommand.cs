using MediatR;

namespace ChatbotAi.Core.CQRS.Commands;

public record AskChatCommand(string message) : INotification;
