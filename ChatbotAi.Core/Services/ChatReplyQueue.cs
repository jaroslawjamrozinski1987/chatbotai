using ChatbotAi.Core.Dto;
using ChatbotAi.Core.Services.Abstractions;

namespace ChatbotAi.Core.Services;

internal class ChatReplyQueue : IChatReplyQueue
{
    private readonly SemaphoreSlim semaphoreSlim = new(1, 1);
    private readonly Queue<ChatMessageDto> queue = new();
    public async Task AddMessage(ChatMessageDto chatMessage)
    {
        await semaphoreSlim.WaitAsync();
        try
        {
            queue.Enqueue(chatMessage);
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    public async Task<ChatMessageDto?> DequeueMessage(CancellationToken cancellationToken)
    {
        await semaphoreSlim.WaitAsync();
        try
        {
            return queue.Dequeue();
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }
}
