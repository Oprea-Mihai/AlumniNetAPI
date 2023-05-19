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
            CreateMap<Event, EventInviteDTO>();
            CreateMap<EventInviteDTO, Event>();
        }
    }
}
