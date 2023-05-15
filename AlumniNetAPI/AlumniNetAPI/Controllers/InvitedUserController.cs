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
    }
}
