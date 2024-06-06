using AlumniNetAPI.Models;

namespace AlumniNetAPI.Repository.Interfaces
{
    public interface IFacultyRepository:IBaseRepository<Faculty>
    {
        Task<Faculty> GetFacultyByIdAsync(int id);
    }
}
