using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlumniNetAPI.Controllers
{
    [Route("api/[controller]")]
    public class InvitedUserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper { get; }

        public InvitedUserController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPut("AnswerEventInvite")]
        public async Task<IActionResult> AnswerEventInvite(int inviteId, bool answer)
        {
            try
            {
                InvitedUser invite = await _unitOfWork.InvitedUserRepository.GetInvitedUserById(inviteId);
                string status = answer == true ? "Accepted" : "Rejected";
                invite.Status = (await _unitOfWork.StatusRepository.GetStatusByName(status)).StatusId;
                await _unitOfWork.InvitedUserRepository.UpdateAsync(invite);
                await _unitOfWork.CompleteAsync();
                return Ok("Successfuly updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        [HttpPost("SendInvitesToEveryone")]
        public async Task<IActionResult> SendInvitesToEveryone(int eventId)
        {
            try
            {
                var users = await _unitOfWork.UserRepository.GetAllAsync();
                foreach (var user in users)
                {
                    InvitedUser invitedUser = new InvitedUser
                    {
                        EventId = eventId,
                        UserId = user.UserId,
                        Status = 1
                    };

                    await _unitOfWork.InvitedUserRepository.AddAsync(invitedUser);
                }
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpPost("SendInvitesByYear")]
        public async Task<IActionResult> SendInvitesByYear(int eventId)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpPost("SendInvitesByFaculty")]
        public async Task<IActionResult> SendInvitesByFaculty(int eventId)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpPost("SendInvitesByFacultyAndYear")]
        public async Task<IActionResult> SendInvitesByFacultyAndYear(int eventId)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpPost("SendInvitesByName")]
        public async Task<IActionResult> SendInvitesByName(int eventId)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
