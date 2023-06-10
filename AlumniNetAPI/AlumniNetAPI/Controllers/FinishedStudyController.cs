using AlumniNetAPI.DTOs;
using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlumniNetAPI.Controllers
{
    [Route("api/[controller]")]
    public class FinishedStudyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FinishedStudyController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetFinishedStudyById")]
        public async Task<IActionResult> GetFinishedStudyById(int id)
        {
            try
            {
                FinishedStudyDetailedDTO study = _mapper.Map<FinishedStudy, FinishedStudyDetailedDTO>
                    (await _unitOfWork.FinishedStudyRepository.GetFinishedStudyByIdAsync(id));
                return Ok(study);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetFinishedStudyByUserId")]
        public async Task<IActionResult> GetFinishedStudyByUserId()
        {
            try
            {
                string? userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
                int profileId = (await _unitOfWork.UserRepository.GetUserByIdAsync(userId)).ProfileId;

                List<FinishedStudy> studies = (await _unitOfWork.FinishedStudyRepository.GetAllDetailed())
                    .Where(x => x.ProfileId == profileId).ToList();

                List<FinishedStudyDetailedDTO> studiesDTO = _mapper.Map<List<FinishedStudy>, List<FinishedStudyDetailedDTO>>(studies);
                return Ok(studiesDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetFinishedStudyByProfileId")]
        public async Task<IActionResult> GetFinishedStudyByProfileId(int profileId)
        {
            try
            {
                List<FinishedStudy> studies = (await _unitOfWork.FinishedStudyRepository.GetAllDetailed()).Where(s => s.ProfileId == profileId).ToList();
                List<FinishedStudyDetailedDTO> studiesDTO = _mapper.Map<List<FinishedStudy>, List<FinishedStudyDetailedDTO>>(studies);
                return Ok(studiesDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateFinishedStudySpecialization")]
        public async Task<IActionResult> UpdateFinishedStudySpecialization(FinishedStudyDTO finishedStudy)
        {
            try
            {
                FinishedStudy updated = _mapper.Map<FinishedStudyDTO, FinishedStudy>(finishedStudy);
                await _unitOfWork.FinishedStudyRepository.UpdateAsync(updated);
                await _unitOfWork.CompleteAsync();
                return Ok(updated);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [Authorize]
        [HttpPut("UpdateFinishedStudy")]
        public async Task<IActionResult> UpdateFinishedStudy([FromBody] FinishedStudyDTO finishedStudy)
        {
            try
            {
                FinishedStudy toUpdate = await _unitOfWork.FinishedStudyRepository
                    .GetFinishedStudyByIdAsync(finishedStudy.FinishedStudyId);

                toUpdate.SpecializationId = finishedStudy.SpecializationId;
                toUpdate.StudyProgramId = finishedStudy.StudyProgramId;
                toUpdate.LearningScheduleId = finishedStudy.LearningScheduleId;
                toUpdate.Year = finishedStudy.Year;

                await _unitOfWork.FinishedStudyRepository.UpdateAsync(toUpdate);
                await _unitOfWork.CompleteAsync();
                return Ok(true);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [Authorize]
        [HttpPost("AddFinishedStudy")]
        public async Task<IActionResult> AddFinishedStudy([FromBody] FinishedStudyDTO finishedStudy)
        {
            try
            {
                FinishedStudy toAdd = new FinishedStudy();

                toAdd.SpecializationId = finishedStudy.SpecializationId;
                toAdd.StudyProgramId = finishedStudy.StudyProgramId;
                toAdd.LearningScheduleId = finishedStudy.LearningScheduleId;
                toAdd.Year = finishedStudy.Year;
                toAdd.ProfileId = (await _unitOfWork.UserRepository
                    .GetUserByIdAsync(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value)).ProfileId;

                await _unitOfWork.FinishedStudyRepository.AddAsync(toAdd);
                await _unitOfWork.CompleteAsync();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [Authorize]
        [HttpDelete("DeleteFinishedStudy")]
        public async Task<IActionResult> DeleteFinishedStudy(int id)
        {
            try
            {
                FinishedStudy toDelete = await _unitOfWork.FinishedStudyRepository.GetFinishedStudyByIdAsync(id);
                await _unitOfWork.FinishedStudyRepository.DeleteAsync(toDelete);
                await _unitOfWork.CompleteAsync();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpGet("GetAllFinishingYears")]
        public async Task<IActionResult> GetAllFinishingYears()
        {
            try
            {
                List<int> allYears = (await _unitOfWork.FinishedStudyRepository.GetAllAsync())
                    .Select(x => x.Year)
                    .Distinct()
                    .ToList();
                return Ok(allYears);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
