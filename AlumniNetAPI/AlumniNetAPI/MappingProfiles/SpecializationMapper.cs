using AlumniNetAPI.DTOs;
using AlumniNetAPI.Models;
using AutoMapper;

namespace AlumniNetAPI.MappingProfiles
{
    public class SpecializationMapper: AutoMapper.Profile
    {
        public SpecializationMapper()
        {
            CreateMap<SpecializationDTO, Specialization>();
            CreateMap<Specialization, SpecializationDTO>();
        }
    }
}
