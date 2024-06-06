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
                Experience toAddexperience = new Experience() ;
                toAddexperience.CompanyName = experience.CompanyName;
                toAddexperience.JobTitle = experience.JobTitle;
                toAddexperience.StartDate = experience.StartDate;
                toAddexperience.EndDate = experience.EndDate;
                toAddexperience.ProfileId = (await _unitOfWork.UserRepository.GetUserByIdAsync(userId)).ProfileId;
                await _unitOfWork.ExperienceRepository.AddAsync(toAddexperience);
                await _unitOfWork.CompleteAsync();
                return Ok(toAddexperience);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetAllExperiencesForUser")]
        public async Task<IActionResult> GetAllExperiencesForUser()
        {
            try
            {
                string? userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
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
        [HttpGet("GetExperienceByProfileId")]
        public async Task<IActionResult> GetExperienceByProfileId(int profileId)
        {
            try
            {
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
        [HttpPut("UpdateExperience")]
        public async Task<IActionResult> UpdateExperience([FromBody]ExperienceDTO experience)
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
        [Authorize]
        [HttpPut("UpdateExperienceCompanyName")]

        public async Task<IActionResult> UpdateExperienceCompanyName(int experienceId, string companyName)
        {
            try
            {
                Experience toUpdate=await _unitOfWork.ExperienceRepository.GetExperienceByIdAsync(experienceId);
                toUpdate.CompanyName=companyName;
                await _unitOfWork.ExperienceRepository.UpdateAsync(toUpdate);
                await _unitOfWork.CompleteAsync();
                return Ok(toUpdate);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("UpdateExperienceJobTitle")]

        public async Task<IActionResult> UpdateExperienceJobTitle(int experienceId, string jobTitle)
        {
            try
            {
                Experience toUpdate = await _unitOfWork.ExperienceRepository.GetExperienceByIdAsync(experienceId);
                toUpdate.JobTitle = jobTitle;
                await _unitOfWork.ExperienceRepository.UpdateAsync(toUpdate);
                await _unitOfWork.CompleteAsync();
                return Ok(toUpdate);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("UpdateExperienceStartDate")]

        public async Task<IActionResult> UpdateExperienceStartDate(int experienceId, int startDate)
        {
            try
            {
                Experience toUpdate = await _unitOfWork.ExperienceRepository.GetExperienceByIdAsync(experienceId);
                toUpdate.StartDate = startDate;
                await _unitOfWork.ExperienceRepository.UpdateAsync(toUpdate);
                await _unitOfWork.CompleteAsync();
                return Ok(toUpdate);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("UpdateExperienceEndDate")]

        public async Task<IActionResult> UpdateExperienceEndDate(int experienceId, int endDate)
        {
            try
            {
                Experience toUpdate = await _unitOfWork.ExperienceRepository.GetExperienceByIdAsync(experienceId);
                toUpdate.EndDate = endDate;
                await _unitOfWork.ExperienceRepository.UpdateAsync(toUpdate);
                await _unitOfWork.CompleteAsync();
                return Ok(toUpdate);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("DeleteExperience")]
        public async Task<IActionResult> DeleteFinishedStudy(int experienceId)
        {
            try
            {
                Experience toDeleteExperience = await _unitOfWork.ExperienceRepository.GetExperienceByIdAsync(experienceId);
                await _unitOfWork.ExperienceRepository.DeleteAsync(toDeleteExperience);
                await _unitOfWork.CompleteAsync();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
