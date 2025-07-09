using ChatbotAi.Core.Types;
using MediatR;

namespace ChatbotAi.Core.CQRS.Queries;

internal record GetResponseQuery(Guid messageId) : IRequest<ChatResponse>;