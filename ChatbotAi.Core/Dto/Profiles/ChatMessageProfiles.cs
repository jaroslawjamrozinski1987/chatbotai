using AutoMapper;
using ChatbotAi.Core.Data.Model;

namespace ChatbotAi.Core.Dto.Profiles;

internal class ChatMessageProfiles : Profile
{
    public ChatMessageProfiles()
    {
        CreateMap<ChatMessage, ChatMessageDto>().ReverseMap();
    }
}
