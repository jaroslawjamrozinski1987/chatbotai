using AutoMapper;
using ChatbotAi.Core.Dto;
using ChatbotAi.Core.Services.Abstractions;
using MediatR;

namespace ChatbotAi.Core.CQRS.Queries.Handlers;

internal class GetHistoryHandler(IChatService chatService, IMapper mapper) : IRequestHandler<GetHistoryQuery, IEnumerable<ChatMessageDto>>
{
    public async Task<IEnumerable<ChatMessageDto>> Handle(GetHistoryQuery request, CancellationToken cancellationToken)
    => mapper.Map<IEnumerable<ChatMessageDto>>(await chatService.GetChatHistoryAsync(
        request.dateFrom,
        request.dateTo,
        request.maxTake));
}
