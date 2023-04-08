﻿using AlumniNetAPI.DTOs;
using AlumniNetAPI.Models;
using AutoMapper;
using Profile = AutoMapper.Profile;

namespace AlumniNetAPI.MappingProfiles
{
    public class ExperienceMapper : Profile
    {
        public ExperienceMapper()
        {
            CreateMap<Experience, ExperienceDTO>();
            CreateMap<ExperienceDTO, Experience>();
        }
    }
}
