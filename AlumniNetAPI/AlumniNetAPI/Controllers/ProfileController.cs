using AlumniNetAPI.DTOs;
using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Profile = AlumniNetAPI.Models.Profile;

namespace AlumniNetAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProfileController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetProfileByUserId")]
        public async Task<IActionResult> GetProfileByUserId(int userId)
        {
            try
            {
                ProfileDTO profileMapping = _mapper.Map<Profile, ProfileDTO>(await _unitOfWork.ProfileRepository.GetProfileByUserIdAsync(userId));
                return Ok(profileMapping);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateProfileByUserId")]
        public async Task<IActionResult> UpdateProfileByUserId(ProfileDTO profile)
        {
            try
            {
                Profile profileToUpdate = await _unitOfWork.ProfileRepository.GetProfileByUserIdAsync(profile.UserId);
                profileToUpdate.Description=profile.Description;
                profileToUpdate.ProfilePicture = profile.ProfilePicture;
                await _unitOfWork.ProfileRepository.UpdateAsync(profileToUpdate);
                await _unitOfWork.CompleteAsync();
                return Ok(profileToUpdate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
