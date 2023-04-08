using AlumniNetAPI.DTOs;
using AlumniNetAPI.Models;
using AutoMapper;

namespace AlumniNetAPI.MappingProfiles
{
    public class FinishedStudyMapper: AutoMapper.Profile
    {
        public FinishedStudyMapper()
        {
            CreateMap<FinishedStudy, FinishedStudyDTO>();
            CreateMap<FinishedStudyDTO, FinishedStudy>();
        }
    }
}
