using AlumniNetAPI.Models;

namespace AlumniNetAPI.Repository.Interfaces
{
    public interface IProfileRepository:IBaseRepository<Profile>
    {
        Task<Profile> GetProfileByUserIdAsync(int id);
        Task<Profile> GetProfileByIdAsync(int id);
    }
}
