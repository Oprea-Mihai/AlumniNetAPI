using AlumniNetAPI.DTOs;
using AlumniNetAPI.Models;
using AutoMapper;
using Google.Apis.Util;
using Profile = AutoMapper.Profile;

namespace AlumniNetAPI.MappingProfiles
{
    public class EventMapper : Profile
    {
        public EventMapper()
        {
            CreateMap<Event, EventDTO>();
            CreateMap<EventDTO, Event>();

            CreateMap<Event, EventInviteDTO>();
            CreateMap<EventInviteDTO, Event>();

            CreateMap<Event, EventWithResultsDTO>()
                .ForMember(dest => dest.Accepted, opt => opt.Ignore())
                .ForMember(dest => dest.Pending, opt => opt.Ignore())
                .ForMember(dest => dest.Rejected, opt => opt.Ignore());
            CreateMap<EventWithResultsDTO, Event>();
        }
    }
}
