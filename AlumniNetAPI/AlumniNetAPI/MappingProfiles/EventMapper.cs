using AlumniNetAPI.DTOs;
using AlumniNetAPI.Models;
using AutoMapper;
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
        }
    }
}
