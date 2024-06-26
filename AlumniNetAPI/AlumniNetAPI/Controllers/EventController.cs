﻿using AlumniNetAPI.DTOs;
using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;
using AlumniNetAPI.Services;
using Amazon.S3;
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
        private IFileStorageService _fileStorageService;


        public EventController(IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileStorageService = fileStorageService;

        }

        [Authorize]
        [HttpGet("GetAllEvents")]
        public async Task<IActionResult> GetAllEvents()
        {
            try
            {
                List<EventDTO> events = _mapper.Map<List<Event>, List<EventDTO>>
                    ((await _unitOfWork.EventRepository.GetAllAsync()).ToList());
                return Ok(events);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpGet("GetAllEventsWithInviteResults")]
        public async Task<IActionResult> GetAllEventsWithInviteResults()
        {
            try
            {
                List<EventWithResultsDTO> eventsWithResults = _mapper.Map<List<Event>, List<EventWithResultsDTO>>
                    ((await _unitOfWork.EventRepository.GetAllAsync()).ToList());



                foreach (EventWithResultsDTO element in eventsWithResults)
                {
                    var eventInvites = await _unitOfWork.InvitedUserRepository.
                        GetInvitationsByEventId(element.EventId);

                    element.Pending = eventInvites.Count(x=>x.Status==1);
                    element.Accepted = eventInvites.Count(x=>x.Status==2);
                    element.Rejected = eventInvites.Count(x=>x.Status==3);

                }
                return Ok(eventsWithResults);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpGet("GetEventsForUser")]
        public async Task<IActionResult> GetEventsForUser()
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

        [Authorize]
        [HttpPost("CreateEvent")]
        public async Task<IActionResult> CreateEvent([FromBody] EventDTO newEvent)
        {
            try
            {
                string? username = User.Claims.FirstOrDefault(c => c.Type == "username")?.Value;

                newEvent.Initiator = username;
                Event ev = _mapper.Map<EventDTO, Event>(newEvent);
                await _unitOfWork.EventRepository.AddAsync(ev);
                await _unitOfWork.CompleteAsync();
                return Ok(ev.EventId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpPut("UpdateEvent")]
        public async Task<IActionResult> UpdateEvent([FromBody] EventDTO eventToUpdate)
        {
            try
            {
                string? username = User.Claims.FirstOrDefault(c => c.Type == "username")?.Value;

                eventToUpdate.Initiator = username;
                Event ev = _mapper.Map<EventDTO, Event>(eventToUpdate);
                await _unitOfWork.EventRepository.UpdateAsync(ev);
                await _unitOfWork.CompleteAsync();
                return Ok(ev.EventId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpPut("UploadEventImage")]
        public async Task<IActionResult> UploadEventImage(IFormFile file)
        {
            try
            {
                if (file.Length == 0)
                    return BadRequest("Empty file!");

                string prefix = $"{DateTime.Now:yyyyMMddHHmmss}-{Guid.NewGuid().ToString().Substring(0, 8)}";
                string key = await _fileStorageService.UploadFileAsync(file, prefix);

                return Ok(key);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("DeleteEvent")]
        public async Task<IActionResult> DeleteEvent(int eventId)
        {

            try
            {
                List<InvitedUser> invites = await _unitOfWork.InvitedUserRepository.GetInvitationsByEventId(eventId);
                foreach (InvitedUser invite in invites)
                    await _unitOfWork.InvitedUserRepository.DeleteAsync(invite);

                Event eventToDelete = await _unitOfWork.EventRepository.GetEventByIdAsync(eventId);
                await _unitOfWork.EventRepository.DeleteAsync(eventToDelete);

                await _unitOfWork.CompleteAsync();

                return Ok("Event successfully deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

    }
}
