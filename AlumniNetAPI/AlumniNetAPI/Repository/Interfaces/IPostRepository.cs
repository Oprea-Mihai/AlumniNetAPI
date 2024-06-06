using AlumniNetAPI.Models;

namespace AlumniNetAPI.Repository.Interfaces
{
    public interface IPostRepository:IBaseRepository<Post>
    {
        Task<List<Post>> GetAllDetailedAsync();

        Task<Post> GetPostByIdAsync(int id);
    }
}
