using AlumniNetAPI.DTOs;
using AlumniNetAPI.Models;

namespace AlumniNetAPI.MappingProfiles
{
    public class UserMapper : AutoMapper.Profile
    {
        public UserMapper()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
        }
    }
}
