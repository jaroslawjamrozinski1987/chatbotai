using ChatbotAi.Core.Dto;

namespace ChatbotAi.Core.Services.Abstractions;

internal interface IChatReplyQueue
{
    Task AddMessage(ChatMessageDto chatMessage);
    Task<ChatMessageDto?> DequeueMessage(CancellationToken cancellationToken);
}
