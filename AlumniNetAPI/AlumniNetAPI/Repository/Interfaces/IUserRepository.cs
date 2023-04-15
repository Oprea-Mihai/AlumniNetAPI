using AlumniNetAPI.Models;

namespace AlumniNetAPI.Repository.Interfaces
{
    public interface IUserRepository:IBaseRepository<User>
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByAuthTokenAsync(string token);
        Task<User> GetUserWithProfileByIdAsync(int id);
    }
}
