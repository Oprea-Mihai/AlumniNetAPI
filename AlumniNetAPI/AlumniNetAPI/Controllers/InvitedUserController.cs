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
        [HttpGet("RefuseEventInvite")]
        public async Task<IActionResult> RefuseEventInvite(int eventId)
        {
            return Ok();
        }

        [Authorize]
        [HttpGet("AcceptEventInvite")]
        public async Task<IActionResult> AcceptEventInvite(int eventId)
        {
            return Ok();
        }
    }
}
