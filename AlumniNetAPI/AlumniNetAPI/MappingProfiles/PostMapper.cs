using AlumniNetAPI.DTOs;
using AlumniNetAPI.Models;

namespace AlumniNetAPI.MappingProfiles
{
    public class PostMapper : AutoMapper.Profile
    {
        public PostMapper()
        {
            CreateMap<Post, PostDTO>();
            CreateMap<PostDTO, Post>();
            CreateMap<PostWithUserDataDTO, Post>();
            CreateMap<Post, PostWithUserDataDTO>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName));
        }
    }
}
