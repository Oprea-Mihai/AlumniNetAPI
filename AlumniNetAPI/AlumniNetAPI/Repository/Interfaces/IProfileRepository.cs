using AlumniNetAPI.Models;

namespace AlumniNetAPI.Repository.Interfaces
{
    public interface IProfileRepository : IBaseRepository<Profile>
    {
        Task<Profile> GetProfileByIdAsync(int id);
        Task<Profile> GetProfileWithStudiesByIdAsync(int id);
    }
}
