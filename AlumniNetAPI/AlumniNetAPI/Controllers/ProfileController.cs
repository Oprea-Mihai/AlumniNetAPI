using AlumniNetAPI.DTOs;
using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;
using AlumniNetAPI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Profile = AlumniNetAPI.Models.Profile;

namespace AlumniNetAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;

        public ProfileController(IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
        }

        [Authorize]
        [HttpGet("GetProfileByUserId")]
        public async Task<IActionResult> GetProfileByUserId()
        {
            try
            {
                string? userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

                UserDTO user = _mapper.Map<User, UserDTO>(await _unitOfWork.UserRepository.GetUserByIdAsync(userId));
                ProfileDTO profileMapping = _mapper.Map<Profile, ProfileDTO>(await _unitOfWork.ProfileRepository.GetProfileByIdAsync(user.ProfileId));
                EntireProfileDTO entireProfileDTO = new EntireProfileDTO();

                entireProfileDTO.Description = profileMapping.Description;
                entireProfileDTO.ProfilePicture = profileMapping.ProfilePicture;
                entireProfileDTO.Facebook = profileMapping.Facebook;
                entireProfileDTO.Instagram = profileMapping.Instagram;
                entireProfileDTO.LinkedIn = profileMapping.LinkedIn;
                entireProfileDTO.FirstName = user.FirstName;
                entireProfileDTO.LastName = user.LastName;
                entireProfileDTO.IsValid = user.IsValid;
                entireProfileDTO.IsAdmin = user.IsAdmin;

                List<Experience> experiences = new List<Experience>();
                experiences = (await _unitOfWork.ExperienceRepository.GetAllAsync()).ToList();

                entireProfileDTO.Experiences = _mapper.Map<List<Experience>, List<ExperienceDTO>>
                    (experiences.Where(exp => exp.ProfileId == user.ProfileId).ToList());


                List<FinishedStudy> studies = new List<FinishedStudy>();
                studies = (await _unitOfWork.FinishedStudyRepository.GetAllDetailed()).ToList();

                entireProfileDTO.FinishedStudiesDetailed = _mapper.Map<List<FinishedStudy>, List<FinishedStudyDetailedDTO>>
                    (studies.Where(x => x.ProfileId == user.ProfileId).ToList());

                return Ok(entireProfileDTO);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetProfileById")]
        public async Task<IActionResult> GetProfileById(int profileId)
        {
            try
            {


                UserDTO user = _mapper.Map<User, UserDTO>(await _unitOfWork.UserRepository.GetUserByProfileIdAsync(profileId));
                ProfileDTO profileMapping = _mapper.Map<Profile, ProfileDTO>(await _unitOfWork.ProfileRepository.GetProfileByIdAsync(profileId));
                EntireProfileDTO entireProfileDTO = new EntireProfileDTO();

                entireProfileDTO.Description = profileMapping.Description;
                entireProfileDTO.ProfilePicture = profileMapping.ProfilePicture;
                entireProfileDTO.FirstName = user.FirstName;
                entireProfileDTO.LastName = user.LastName;
                entireProfileDTO.IsAdmin = user.IsAdmin;
                entireProfileDTO.IsValid = user.IsValid;
                entireProfileDTO.Facebook = profileMapping.Facebook;
                entireProfileDTO.Instagram = profileMapping.Instagram;
                entireProfileDTO.LinkedIn = profileMapping.LinkedIn;

                List<Experience> experiences = new List<Experience>();
                experiences = (await _unitOfWork.ExperienceRepository.GetAllAsync()).ToList();

                entireProfileDTO.Experiences = _mapper.Map<List<Experience>, List<ExperienceDTO>>
                    (experiences.Where(exp => exp.ProfileId == user.ProfileId).ToList());


                List<FinishedStudy> studies = new List<FinishedStudy>();
                studies = (await _unitOfWork.FinishedStudyRepository.GetAllDetailed()).ToList();

                entireProfileDTO.FinishedStudiesDetailed = _mapper.Map<List<FinishedStudy>, List<FinishedStudyDetailedDTO>>
                    (studies.Where(x => x.ProfileId == user.ProfileId).ToList());

                return Ok(entireProfileDTO);
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
                profileToUpdate.Description = profile.Description;
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

        [Authorize]
        [HttpPut("UpdateProfilePictureByUserId")]
        public async Task<IActionResult> UpdateProfilePictureByUserId(IFormFile file)
        {
            try
            {

                string? userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

                Profile profileToUpdate = (await _unitOfWork.UserRepository.GetUserWithProfileByIdAsync(userId)).Profile;

                if (profileToUpdate.ProfilePicture != null || profileToUpdate.ProfilePicture == "")
                    await _fileStorageService.DeleteFileByKeyAsync(profileToUpdate.ProfilePicture);

                string prefix = $"{DateTime.Now:yyyyMMddHHmmss}-{Guid.NewGuid().ToString().Substring(0, 8)}";
                string key = await _fileStorageService.UploadFileAsync(file, prefix);
                profileToUpdate.ProfilePicture = key;

                await _unitOfWork.ProfileRepository.UpdateAsync(profileToUpdate);
                await _unitOfWork.CompleteAsync();
                return Ok(profileToUpdate.ProfilePicture);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("DeleteProfilePictureByUserId")]
        public async Task<IActionResult> DeleteProfilePictureByUserId()
        {
            try
            {
                string? userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
                Profile profile = (await _unitOfWork.UserRepository.GetUserWithProfileByIdAsync(userId)).Profile;
                if (profile.ProfilePicture != null)
                    await _fileStorageService.DeleteFileByKeyAsync(profile.ProfilePicture);

                profile.ProfilePicture = null;

                await _unitOfWork.ProfileRepository.UpdateAsync(profile);
                await _unitOfWork.CompleteAsync();
                return Ok("Successfully deleted!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("UpdateProfileDescriptionByUserId")]
        public async Task<IActionResult> UpdateProfileDescriptionByUserId(string profileDescription)
        {
            try
            {
                string? userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
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

        [Authorize]
        [HttpPut("UpdateSocialMediaLinksByUserId")]
        public async Task<IActionResult> UpdateSocialMediaLinksByUserId(string instagram, string facebook, string linkedIn)
        {
            try
            {
                string? userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
                Profile profileToUpdate = (await _unitOfWork.UserRepository.GetUserWithProfileByIdAsync(userId)).Profile;
                profileToUpdate.Facebook = facebook;
                profileToUpdate.LinkedIn = linkedIn;
                profileToUpdate.Instagram = instagram;
                await _unitOfWork.ProfileRepository.UpdateAsync(profileToUpdate);
                await _unitOfWork.CompleteAsync();
                return Ok("Social media updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
