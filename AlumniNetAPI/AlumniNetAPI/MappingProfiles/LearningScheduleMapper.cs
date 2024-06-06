using AlumniNetAPI.DTOs;
using AlumniNetAPI.Models;
using AutoMapper;

namespace AlumniNetAPI.MappingProfiles
{
    public class LearningScheduleMapper: AutoMapper.Profile
    {
        public LearningScheduleMapper()
        {
            CreateMap<LearningSchedule, LearningScheduleDTO>();
            CreateMap<LearningScheduleDTO, LearningSchedule>();
        }
    }
}
