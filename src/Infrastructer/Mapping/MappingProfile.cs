using AutoMapper;
using Core.DTOs.Event;
using Core.DTOs.Invitation;
using Core.DTOs.Participant;
using Core.Entities;

namespace Infrastructure.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateEventDto, Event>();
            CreateMap<UpdateEventDto, Event>();
            CreateMap<ViewEventWithParticipantsDto, Event>().ReverseMap();
            CreateMap<ViewEventDto, Event>().ReverseMap();
            CreateMap<Invitation, InvitationDto>()
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
                .ForMember(dest => dest.IsAccepted, opt => opt.MapFrom(src => src.IsAccepted))
                .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.Event.EventName))
                .ForMember(dest => dest.EventStartDate, opt => opt.MapFrom(src => src.Event.StartDate))
                .ForMember(dest => dest.EventEndDate, opt => opt.MapFrom(src => src.Event.EndDate))
                .ForMember(dest => dest.Timezone, opt => opt.MapFrom(src => src.Event.Timezone))
                .ForMember(dest => dest.Organizer, opt => opt.MapFrom(src => src.Organizer.UserName))
                .ForMember(dest => dest.Receiver, opt => opt.MapFrom(src => src.Receiver.UserName));  
            ;  

            CreateMap<ParticipantDto,Participant>().ReverseMap();
        }
    }
}
