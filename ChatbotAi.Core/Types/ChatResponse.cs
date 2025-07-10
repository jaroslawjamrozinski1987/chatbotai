namespace ChatbotAi.Core.Types;

public record ChatResponse(bool isReady, bool stopped, ChatResponseBody? body);

public record ChatResponseBody(Guid id, string message);
