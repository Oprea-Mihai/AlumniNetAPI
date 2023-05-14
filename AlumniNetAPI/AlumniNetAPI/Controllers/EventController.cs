using AlumniNetAPI.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlumniNetAPI.Controllers
{
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper { get; }

        public EventController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("GetAllEvents")]
        public async Task<IActionResult> GetAllEvents()
        {
            return Ok();
        }

        [Authorize]
        [HttpGet("GetEventsForUser")]
        public async Task<IActionResult> GetAllEventsForUser()
        {
            return Ok();
        }
    }
}
