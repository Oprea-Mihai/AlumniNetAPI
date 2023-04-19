using AlumniNetAPI.DTOs;
using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AlumniNetAPI.Controllers
{
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostController(IUnitOfWork unitOfWork, IMapper mapper)
        {
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

        [HttpGet("GetBatchOfPostsSorted")]
        public async Task<IActionResult> GetBatchOfPostsSorted(int batchSize, int currentIndex)
        {
            try
            {
                List<Post> posts = (await _unitOfWork.PostRepository.GetAllDetailedAsync()).ToList();
                List<PostWithUserDataDTO> descendingOrderedPosts = _mapper.Map<List<Post>, List<PostWithUserDataDTO>>
                    (posts.OrderByDescending(p => p.PostingDate).ToList());

                List <PostWithUserDataDTO> batchToDeliver=descendingOrderedPosts.Skip(batchSize * currentIndex).Take(batchSize).ToList();

                return Ok(batchToDeliver);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
