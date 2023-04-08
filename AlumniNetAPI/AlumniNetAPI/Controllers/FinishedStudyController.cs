using AlumniNetAPI.DTOs;
using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;
using AutoMapper;
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
                FinishedStudyDetailedDTO study =_mapper.Map<FinishedStudy,FinishedStudyDetailedDTO> 
                    (await _unitOfWork.FinishedStudyRepository.GetFinishedStudyByIdAsync(id));
                return Ok(study);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetFinishedStudyByUserId")]
        public async Task<IActionResult> GetFinishedStudyByUserId(int id)
        {
            try
            {
                int profileId = (await _unitOfWork.UserRepository.GetUserByIdAsync(id)).ProfileId;
                List<FinishedStudy> study=(await _unitOfWork.FinishedStudyRepository.GetAllAsync()).Where(x=>x.ProfileId==profileId).ToList();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetFinishedStudyByProfileId")]
        public async Task<IActionResult> GetFinishedStudyByProfileId()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateFinishedStudy")]
        public async Task<IActionResult>UpdateFinishedStudySpecialization(FinishedStudyDTO finishedStudy)
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
    }
}
