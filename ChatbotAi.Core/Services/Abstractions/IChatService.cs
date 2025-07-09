using ChatbotAi.Core.Data.Model;
using ChatbotAi.Core.Types;

namespace ChatbotAi.Core.Services.Abstractions;

internal interface IChatService
{
    Task<Guid> AddUserMessageAsync(string content);

    Task<ChatResponse> GenerateBotReplyAsync(Guid userMessageId, CancellationToken cancellationToken);

    Task SetRatingAsync(Guid messageId, byte rating);

    Task<List<ChatMessage>> GetChatHistoryAsync(DateTime dateFrom, DateTime dateTo, int count);
}
