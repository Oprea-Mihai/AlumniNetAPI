using AlumniNetAPI.DTOs;
using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;
using AlumniNetAPI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace AlumniNetAPI.Controllers
{
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private IFileStorageService _fileStorageService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PostController(IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllPostsSorted")]
        public async Task<IActionResult> GetAllPostsSorted()
        {
            try
            {
                List<Post> posts = (await _unitOfWork.PostRepository.GetAllAsync()).ToList();
                List<PostDTO> descendingOrderedPosts = _mapper.Map<List<Post>, List<PostDTO>>
                    (posts.OrderByDescending(p => p.PostingDate).ToList());

                return Ok(descendingOrderedPosts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetBatchOfPostsSorted")]
        public async Task<IActionResult> GetBatchOfPostsSorted(int batchSize, int currentIndex)
        {
            try
            {
                List<Post> posts = (await _unitOfWork.PostRepository.GetAllDetailedAsync()).ToList();
                List<PostWithUserDataDTO> descendingOrderedPosts = _mapper.Map<List<Post>, List<PostWithUserDataDTO>>
                    (posts.OrderByDescending(p => p.PostingDate).ToList());

                List<PostWithUserDataDTO> batchToDeliver = descendingOrderedPosts.Skip(batchSize * currentIndex).Take(batchSize).ToList();

                return Ok(batchToDeliver);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetPostsByUserId")]

        public async Task<IActionResult> GetPostsByUserId()
        {
            try
            {
                string? userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
                List<Post> posts = new List<Post>();
                posts = (await _unitOfWork.PostRepository.GetAllAsync()).ToList();

                List<PostDTO> userPosts = _mapper.Map<List<Post>, List<PostDTO>>
                    (posts.Where(post => post.UserId == userId).OrderByDescending(post => post.PostingDate).ToList());

                return Ok(userPosts);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        [HttpPost("AddNewPostForUser")]
        public async Task<IActionResult> AddNewPostForUser([FromBody] PostDTO post)
        {
            try
            {
                string? userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
                Post postMapping = _mapper.Map<PostDTO, Post>(post);
                postMapping.UserId = (await _unitOfWork.UserRepository.GetUserByIdAsync(userId)).UserId;
                await _unitOfWork.PostRepository.AddAsync(postMapping);
                await _unitOfWork.CompleteAsync();
                return Ok("Post created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("UploadPostImage")]
        public async Task<IActionResult> UploadPostImage(IFormFile file)
        {
            try
            {
                if (file.Length == 0)
                    return BadRequest("Empty file!");
                string? userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

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
        [HttpPost("UpdatePost")]
        public async Task<IActionResult> UpdatePost(PostDTO post)
        {
            try
            {
                Post postToUpdate = await _unitOfWork.PostRepository.GetPostByIdAsync(post.PostId);

                postToUpdate.Text = post.Text;
                postToUpdate.Title = post.Title;
                postToUpdate.Image = post.Image;

                await _unitOfWork.PostRepository.UpdateAsync(postToUpdate);
                await _unitOfWork.CompleteAsync();
                return Ok(postToUpdate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("UpdatePostImage")]
        public async Task<IActionResult> UpdatePostImage(int postId, string postImage)
        {
            try
            {
                Post postToUpdate = (await _unitOfWork.PostRepository.GetPostByIdAsync(postId));
                postToUpdate.Image = postImage;
                await _unitOfWork.PostRepository.UpdateAsync(postToUpdate);
                await _unitOfWork.CompleteAsync();
                return Ok(postToUpdate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("UpdatePostText")]
        public async Task<IActionResult> UpdatePostText(int postId, string postText)
        {
            try
            {
                Post postToUpdate = (await _unitOfWork.PostRepository.GetPostByIdAsync(postId));
                postToUpdate.Text = postText;
                await _unitOfWork.PostRepository.UpdateAsync(postToUpdate);
                await _unitOfWork.CompleteAsync();
                return Ok(postToUpdate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("UpdatePostTitle")]
        public async Task<IActionResult> UpdatePostTitle(int postId, string postTitle)
        {
            try
            {
                Post postToUpdate = (await _unitOfWork.PostRepository.GetPostByIdAsync(postId));
                postToUpdate.Title = postTitle;
                await _unitOfWork.PostRepository.UpdateAsync(postToUpdate);
                await _unitOfWork.CompleteAsync();
                return Ok(postToUpdate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("DeletePost")]

        public async Task<IActionResult> DeletePost(int postId)
        {
            try
            {
                Post postToDelete = (await _unitOfWork.PostRepository.GetPostByIdAsync(postId));

                if (postToDelete == null)
                {
                    return NotFound();
                }

                await _unitOfWork.PostRepository.DeleteAsync(postToDelete);
                await _unitOfWork.CompleteAsync();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
