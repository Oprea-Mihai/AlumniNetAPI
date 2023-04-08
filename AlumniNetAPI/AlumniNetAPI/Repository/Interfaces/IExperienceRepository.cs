using AlumniNetAPI.Models;

namespace AlumniNetAPI.Repository.Interfaces
{
    public interface IExperienceRepository:IBaseRepository<Experience>
    {
        Task<Experience> GetExperienceByIdAsync(int id);
    }
}
