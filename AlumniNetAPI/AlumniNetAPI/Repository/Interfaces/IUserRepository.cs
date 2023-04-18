using AlumniNetAPI.Models;

namespace AlumniNetAPI.Repository.Interfaces
{
    public interface IUserRepository:IBaseRepository<User>
    {
        Task<User> GetUserByIdAsync(string id);
        Task<User> GetUserWithProfileByIdAsync(string id);
    }
}
