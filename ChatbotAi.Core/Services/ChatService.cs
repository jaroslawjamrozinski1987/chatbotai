using ChatbotAi.Core.Data.Model;
using ChatbotAi.Core.Data;
using ChatbotAi.Core.Services.Abstractions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ChatbotAi.Core.Types;
using AutoMapper;
using ChatbotAi.Core.Dto;

namespace ChatbotAi.Core.Services;

internal class ChatService(ChatDbContext chatDbContext, IChatReplyQueue replyQueue, IMapper mapper, ICacheService cacheService, TimeProvider timeProvider) : IChatService
{
    public async Task<Guid> AddUserMessageAsync(string content)
    {
        using var transaction = await chatDbContext.Database.BeginTransactionAsync();

        try
        {
            var message = new ChatMessage
            {
                Content = content,
                IsFromUser = true,
                Timestamp = timeProvider.GetUtcNow().UtcDateTime
            };

            chatDbContext.Messages.Add(message);
            await chatDbContext.SaveChangesAsync();

            var msg = mapper.Map<ChatMessageDto>(message);
            await replyQueue.AddMessage(msg);

            await transaction.CommitAsync();
            return message.Id;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
        
    }

    public async Task<ChatResponse> GenerateBotReplyAsync(Guid userMessageId, CancellationToken cancellationToken)
    {      
        var generatedText = await SimulateTypingAsync(
            GetSimulatedBotResponse(), cancellationToken);

        var botMessage = new ChatMessage
        {
            Content = generatedText,
            IsFromUser = false,
            Timestamp = timeProvider.GetUtcNow().UtcDateTime,
            ParentMessageId = userMessageId
        };

        chatDbContext.Messages.Add(botMessage);
        await chatDbContext.SaveChangesAsync();
        return new ChatResponse(true, new ChatResponseBody(botMessage.Id, botMessage.Content));
    }

    public async Task SetRatingAsync(Guid messageId, byte rating)
    {
        var message = await chatDbContext.Messages
            .Where(m => !m.IsFromUser && m.Id == messageId && !m.IsFromUser)
            .FirstOrDefaultAsync();

        if (message is not null)
        {
            message.Rating = rating;
            await chatDbContext.SaveChangesAsync();
        }
    }

    public async Task<List<ChatMessage>> GetChatHistoryAsync()
    {
        return await chatDbContext.Messages
            .OrderBy(m => m.Timestamp)
            .ToListAsync();
    }

    public Task<List<ChatMessage>> GetChatHistoryAsync(DateTime dateFrom, DateTime dateTo, int count)
    => chatDbContext.Messages
        .Where(m => m.Timestamp >= dateFrom && m.Timestamp <= dateTo)
        .Take(count)
            .OrderBy(m => m.Timestamp)
            .ToListAsync();

    private static async Task<string> SimulateTypingAsync(string fullText, CancellationToken cancellationToken)
    {
        var sb = new StringBuilder();

        foreach (char c in fullText)
        {
            if (cancellationToken.IsCancellationRequested)
                break;

            sb.Append(c);
            await Task.Delay(30, cancellationToken);
        }

        return sb.ToString();
    }

    private static string GetSimulatedBotResponse()
    {
        var responses = new[]
        {
            "Dziękuję za pytanie. Tak, mogę Ci pomóc!",
            "To ciekawe zagadnienie. W skrócie wygląda to tak:\n\n1. Pierwszy punkt\n2. Drugi punkt\n\nA teraz wyjaśnię szczegóły...",
            "Oczywiście! Już tłumaczę:\n\nLorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua..."
        };

        var rand = new Random();
        return responses[rand.Next(responses.Length)];
    }

    public async Task<ChatResponse> GetResponse(Guid userMessageId)
    {
        var response = await chatDbContext.Messages.FirstOrDefaultAsync(a=>a.ParentMessageId == userMessageId && a.IsFromUser);

        if(response is null)
        {
            return new ChatResponse(false, null);
        }

        return new ChatResponse(true, new ChatResponseBody(response.Id, response.Content));
    }
}