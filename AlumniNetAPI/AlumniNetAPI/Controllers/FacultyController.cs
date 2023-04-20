using AlumniNetAPI.DTOs;
using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlumniNetAPI.Controllers
{
    [Route("api/[controller]")]
    public class FacultyController : ControllerBase    

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FacultyController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllFaculties")]
        public async Task<IActionResult> GetAllFaculties()
        {
            try
            {
                var faculties =_mapper.Map<IEnumerable<Faculty>,IEnumerable<FacultyDTO>>
                    (await _unitOfWork.FacultyRepository.GetAllAsync());
                return Ok(faculties);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [Authorize]
        [HttpGet("GetFacultiesSearchSuggestions")]
        public async Task<IActionResult> GetFacultiesSearchSuggestions(string searchedString)
        {
            try
            {
                List<FacultyDTO>faculties =_mapper.Map<List<Faculty>,List<FacultyDTO>>
                    ((await _unitOfWork.FacultyRepository.GetAllAsync())
                    .Where(x => x.FacultyName.ToLower().Contains(searchedString.ToLower()))
                    .ToList());
                   
                return Ok(faculties);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpGet("GetFacultyById")]
        public async Task<IActionResult> GetFacultyById(int id)
        {
            try
            {
                FacultyDTO faculty = _mapper.Map<Faculty, FacultyDTO>
                    (await _unitOfWork.FacultyRepository.GetFacultyByIdAsync(id));
                return Ok(faculty);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }     

    }
}
