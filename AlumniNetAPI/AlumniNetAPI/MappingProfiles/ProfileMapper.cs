using AlumniNetAPI.DTOs;
using AutoMapper;

namespace AlumniNetAPI.MappingProfiles
{
    public class ProfileMapper:Profile
    {
        public ProfileMapper()
        {
            CreateMap<Models.Profile, ProfileDTO>();   
            CreateMap<ProfileDTO,Models.Profile>();   
        }
    }
}
