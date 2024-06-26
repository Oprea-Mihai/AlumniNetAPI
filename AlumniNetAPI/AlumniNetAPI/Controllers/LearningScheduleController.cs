﻿using AlumniNetAPI.DTOs;
using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlumniNetAPI.Controllers
{
    [Route("api/[controller]")]
    public class LearningScheduleController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LearningScheduleController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("GetAllLearningSchedules")]
        public async Task<IActionResult> GetAllLearningSchedules()
        {
            try
            {
                var learningSchedules = _mapper.Map<IEnumerable<LearningSchedule>, IEnumerable<LearningScheduleDTO>>
                    (await _unitOfWork.LearningScheduleRepository.GetAllAsync());
                return Ok(learningSchedules);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpGet("GetLearningScheduleById")]
        public async Task<IActionResult> GetLearningScheduleById(int lerningScheduleId)
        {
            try
            {
                LearningScheduleDTO learningSchedule = _mapper.Map<LearningSchedule, LearningScheduleDTO>
                    (await _unitOfWork.LearningScheduleRepository.GetLearningScheduleByIdAsync(lerningScheduleId));
                return Ok(learningSchedule);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
