using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlumniNetAPI.Repository
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(AlumniNetAppContext context) : base(context)
        {
        }

        public async Task<List<Post>> GetAllDetailedAsync()
        {
            List<Post> posts = await _dbSet
                .Include(p => p.User).ThenInclude(u => u.Profile).ToListAsync();
            return posts;
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            Post post = await _dbSet.SingleAsync(p => p.PostId == id);
            return post;
        }

    }
}
