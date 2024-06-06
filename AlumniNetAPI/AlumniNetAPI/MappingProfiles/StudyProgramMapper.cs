using AlumniNetAPI.DTOs;
using AlumniNetAPI.Models;
using AutoMapper;

namespace AlumniNetAPI.MappingProfiles
{
    public class StudyProgramMapper: AutoMapper.Profile
    {
        public StudyProgramMapper()
        {
            CreateMap<StudyProgramDTO, StudyProgram>();
            CreateMap<StudyProgram, StudyProgramDTO>();
        }
    }
}
