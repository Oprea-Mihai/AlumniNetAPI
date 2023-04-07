using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;

namespace AlumniNetAPI.Repository
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(AlumniNetAppContext context) : base(context)
        {
        }
    }
}
