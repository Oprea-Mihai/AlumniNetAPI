using AlumniNetAPI.DTOs;
using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlumniNetAPI.Controllers
{
    [Route("api/[controller]")]
    public class ExperienceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper { get; }

        public ExperienceController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetExperienceById")]
        public async Task<IActionResult> GetExperienceById(int id)
        {
            try
            {
                Experience exp= await _unitOfWork.ExperienceRepository.GetExperienceByIdAsync(id);
                ExperienceDTO experience = _mapper.Map<Experience, ExperienceDTO>(exp);
                return Ok(experience);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("AddNewExperienceForUser")]
        public async Task<IActionResult> AddNewExperienceForUser([FromBody]ExperienceDTO experience)
        {
            try
            {
                string? userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
                Experience expMapping = _mapper.Map<ExperienceDTO, Experience>(experience);
                expMapping.ProfileId = (await _unitOfWork.UserRepository.GetUserByIdAsync(userId)).ProfileId;
                await _unitOfWork.ExperienceRepository.AddAsync(expMapping);
                await _unitOfWork.CompleteAsync();
                return Ok(expMapping);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetAllExperiencesForUser")]
        public async Task<IActionResult> GetAllExperiencesForUser(string userId)
        {
            try
            {
                int profileId= (await _unitOfWork.UserRepository.GetUserByIdAsync(userId)).ProfileId;
                List<Experience> experiences = new List<Experience>();
                experiences = (await _unitOfWork.ExperienceRepository.GetAllAsync()).ToList();


                List<ExperienceDTO> userExp = _mapper.Map<List<Experience>, List<ExperienceDTO>>
                    (experiences.Where(exp => exp.ProfileId == profileId).ToList());
                return Ok(userExp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("UpdateExperience")]
        public async Task<IActionResult> UpdateExperience(ExperienceDTO experience)
        {
            try
            {
                Experience toUpdate = await _unitOfWork.ExperienceRepository.GetExperienceByIdAsync(experience.ExperienceId);
                toUpdate.EndDate = experience.EndDate;
                toUpdate.StartDate = experience.StartDate;
                toUpdate.CompanyName=experience.CompanyName;
                toUpdate.JobTitle=experience.JobTitle;
                await _unitOfWork.ExperienceRepository.UpdateAsync(toUpdate);
                await _unitOfWork.CompleteAsync();
                return Ok(toUpdate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
