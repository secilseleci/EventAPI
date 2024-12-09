using AutoMapper;
using Core.DTOs.Event;
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

            CreateMap<ParticipantDto,Participant>().ReverseMap();
        }
    }
}
