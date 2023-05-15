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

                List<InvitedUser> eventInvites = (await _unitOfWork.InvitedUserRepository.GetAllAsync())
                    .Where(e => e.UserId == userId).ToList();

                List<EventInviteDTO> userEvents = new List<EventInviteDTO>();

                foreach (InvitedUser userInv in eventInvites)
                {
                    EventInviteDTO invite = _mapper.Map<Event, EventInviteDTO>
                        (await _unitOfWork.EventRepository.GetEventByIdAsync(userInv.EventId));
                    invite.Status = (await _unitOfWork.StatusRepository.GetStatusById(userInv.Status)).StatusName;
                    invite.InviteId = userInv.InvitedUserId;
                    userEvents.Add(invite);
                }

                return Ok(userEvents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
