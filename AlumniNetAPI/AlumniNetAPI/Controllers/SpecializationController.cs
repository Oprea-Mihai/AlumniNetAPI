using AlumniNetAPI.DTOs;
using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlumniNetAPI.Controllers
{
    [Route("api/[controller]")]
    public class SpecializationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SpecializationController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllSpecializations")]
        public async Task<IActionResult> GetAllSpecializations()
        {
            try
            {
                var specializations = _mapper.Map<IEnumerable<Specialization>, IEnumerable<SpecializationDTO>>
                    (await _unitOfWork.SpecializationRepository.GetAllAsync());
                return Ok(specializations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [Authorize]
        [HttpGet("GetSpecializationsByFacultyId")]
        public async Task<IActionResult> GetSpecializationsByFacultyId(int facultyId)
        {
            try
            {
                List<SpecializationDTO>specializations= _mapper.Map<List<Specialization>, List<SpecializationDTO>>
                    (await _unitOfWork.SpecializationRepository.GetSpecializationsByFacultyIdAsync(facultyId));
                return Ok(specializations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [Authorize]
        [HttpGet("GetSpecializationsByFacultyAndSearchString")]
        public async Task<IActionResult> GetSpecializationsByFacultyAndSearchString(int facultyId,string searchedString)
        {
            try
            {
                List<SpecializationDTO> specializations = _mapper.Map<List<Specialization>, List<SpecializationDTO>>
                    (await _unitOfWork.SpecializationRepository.GetSpecializationsByFacultyIdAsync(facultyId))
                    .Where(x => x.SpecializationName.ToLower().Contains(searchedString.ToLower()))
                    .ToList(); 
                return Ok(specializations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpGet("GetSpecializationsById")]
        public async Task<IActionResult> GetSpecializationsById(int id)
        {
            try
            {
                SpecializationDTO specialization = _mapper.Map<Specialization, SpecializationDTO>
                    (await _unitOfWork.SpecializationRepository.GetSpecializationByIdAsync(id));
                return Ok(specialization);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
