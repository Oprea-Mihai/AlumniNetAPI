using AlumniNetAPI.DTOs;
using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
            try
            {
                List<Event> events = (await _unitOfWork.EventRepository.GetAllAsync()).ToList();
                return Ok(events);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [Authorize]
        [HttpGet("GetEventsForUser")]
        public async Task<IActionResult> GetAllEventsForUser()
        {
            try
            {
                string? userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

                List<int> eventIds = (await _unitOfWork.InvitedUserRepository.GetAllAsync())
                    .Where(e => e.UserId == userId)
                    .Select(e => e.EventId).ToList();

                List<EventDTO> userEvents = new List<EventDTO>();

                foreach (int id in eventIds)
                {
                    userEvents.Add(_mapper.Map<Event, EventDTO>
                        (await _unitOfWork.EventRepository.GetEventByIdAsync(id)));
                }

                return Ok(userEvents);//TEST IF THIS WORKS
                                      //CONNECT WITH CLIENT
                                      //!REMOVE!
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
