using AlumniNetAPI.DTOs;
using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AlumniNetAPI.Controllers
{
    [Route("api/[controller]")]
    public class StudyProgramController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public StudyProgramController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllStudyProgram")]
        public async Task<IActionResult> GetAllStudyProgram()
        {
            try
            {
                var studyPrograms = _mapper.Map<IEnumerable<StudyProgram>, IEnumerable<StudyProgramDTO>>
                    (await _unitOfWork.StudyProgramRepository.GetAllAsync());
                return Ok(studyPrograms);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpGet("GetStudyProgramById")]
        public async Task<IActionResult> GetStudyProgramById(int studyProgramId)
        {
            try
            {
                StudyProgramDTO studyProgram = _mapper.Map<StudyProgram, StudyProgramDTO>
                    (await _unitOfWork.StudyProgramRepository.GetStudyProgramByIdAsync(studyProgramId));
                return Ok(studyProgram);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
