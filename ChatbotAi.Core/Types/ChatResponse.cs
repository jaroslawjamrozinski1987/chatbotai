namespace ChatbotAi.Core.Types;

public record ChatResponse(bool isReady, ChatResponseBody? body);

public record ChatResponseBody(Guid id, string message);
