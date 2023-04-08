using AlumniNetAPI.DTOs;
using AlumniNetAPI.Models;
using AutoMapper;

namespace AlumniNetAPI.MappingProfiles
{
    public class FacultyMapper: AutoMapper.Profile
    {
        public FacultyMapper()
        {
            CreateMap<FacultyDTO, Faculty>();
            CreateMap<Faculty, FacultyDTO>();
        }
    }
}
