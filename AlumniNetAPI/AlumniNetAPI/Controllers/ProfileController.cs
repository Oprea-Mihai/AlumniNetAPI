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
        public async Task<IActionResult> GetProfileByUserId(string userId)
        {
            try
            {
                int profileId = (await _unitOfWork.UserRepository.GetUserByIdAsync(userId)).ProfileId;
                ProfileDTO profileMapping = _mapper.Map<Profile, ProfileDTO>(await _unitOfWork.ProfileRepository.GetProfileByIdAsync(profileId));
                return Ok(profileMapping);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateProfileByUserId")]
        public async Task<IActionResult> UpdateProfileByUserId(ProfileDTO profile, string userId)
        {
            try
            {
                Profile profileToUpdate = (await _unitOfWork.UserRepository.GetUserByIdAsync(userId)).Profile;
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

        [HttpPut("UpdateProfilePictureByUserId")]
        public async Task<IActionResult> UpdateProfilePictureByUserId(string profilePicture, string userId)
        {
            try
            {
               
                Profile profileToUpdate = (await _unitOfWork.UserRepository.GetUserWithProfileByIdAsync(userId)).Profile;
                profileToUpdate.ProfilePicture = profilePicture;
                await _unitOfWork.ProfileRepository.UpdateAsync(profileToUpdate);
                await _unitOfWork.CompleteAsync();
                return Ok(profileToUpdate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateProfileDescriptionByUserId")]
        public async Task<IActionResult> UpdateProfileDescriptionByUserId(string profileDescription, string userId)
        {
            try
            {

                Profile profileToUpdate = (await _unitOfWork.UserRepository.GetUserWithProfileByIdAsync(userId)).Profile;
                profileToUpdate.Description = profileDescription;
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
