using ChatbotAi.Core.Dto;
using MediatR;

namespace ChatbotAi.Core.CQRS.Queries;

public record GetHistoryQuery(DateTime dateFrom, DateTime dateTo, int maxTake) : IRequest<IEnumerable<ChatMessageDto>>;
