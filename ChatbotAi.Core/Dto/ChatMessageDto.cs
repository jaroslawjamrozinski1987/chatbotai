namespace ChatbotAi.Core.Dto;

public class ChatMessageDto
{
    public Guid Id { get; set; }

    public string Content { get; set; } = string.Empty;

    public bool IsFromUser { get; set; }

    public DateTime Timestamp { get; set; }

    public int? Rating { get; set; }

    public Guid? ParentMessageId { get; set; }
}