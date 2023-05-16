using AlumniNetAPI.DTOs;
using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AlumniNetAPI.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = (await _unitOfWork.UserRepository.GetAllAsync()).ToList();
                List<UserDTO> mappedUsers = _mapper.Map<List<User>, List<UserDTO>>(users.ToList());

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById()
        {
            try
            {
                string? userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
                User user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("UserValidation")]

        public async Task<IActionResult> UserValidation(string userId)
        {
            try
            {
                User userUpdate = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);
                userUpdate.IsValid = true;
                await _unitOfWork.UserRepository.UpdateAsync(userUpdate);
                await _unitOfWork.CompleteAsync();
                return Ok(userUpdate);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetUserSearchResults")]
        public async Task<IActionResult> GetUserSearchResults(string searchedString)
        {
            try
            {
                List<UserSearchResultDTO> users =
                    (await _unitOfWork.UserRepository.GetAllAsync())
                    .Where(x => x.FirstName.ToLower().Contains(searchedString.ToLower())
                    || x.LastName.ToLower().Contains(searchedString.ToLower())
                    && x.IsValid == true)
                     .Select(x => new UserSearchResultDTO
                     {
                         ProfileId = x.ProfileId,
                         FirstName = x.FirstName,
                         LastName = x.LastName
                     })
                     .ToList();

                foreach (var user in users)
                {
                    Models.Profile profile = await _unitOfWork.ProfileRepository.GetProfileWithStudiesByIdAsync(user.ProfileId);
                    user.ProfilePicture = profile.ProfilePicture;
                    user.FacultyName = profile.FinishedStudies.First().Specialization.Faculty.FacultyName;
                    user.GraduationYear = profile.FinishedStudies.First().Year;
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] UserDTO newUser)
        {
            try
            {
                string? userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
                if (userId != null)
                {
                    User user = _mapper.Map<UserDTO, User>(newUser);
                    user.Profile = new Models.Profile();
                    user.UserId = userId;
                    await _unitOfWork.UserRepository.AddAsync(user);
                    await _unitOfWork.CompleteAsync();
                    return Ok(user);
                }
                return BadRequest("User not recognized by authentication provider");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
