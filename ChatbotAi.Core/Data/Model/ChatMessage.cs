namespace ChatbotAi.Core.Data.Model;

internal class ChatMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Content { get; set; } = string.Empty;

    public bool IsFromUser { get; set; }

    public byte? Rating { get; set; } = 0;

    public DateTime Timestamp { get; set; }

    public Guid? ParentMessageId { get; set; }

    public ChatMessage? ParentMessage { get; set; }

    public List<ChatMessage> Replies { get; set; } = new();
}