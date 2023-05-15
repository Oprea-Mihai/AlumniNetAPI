using AlumniNetAPI.Models;

namespace AlumniNetAPI.Repository.Interfaces
{
    public interface IStatusRepository : IBaseRepository<Status>
    {
        Task<Status> GetStatusById(int id);
        Task<Status> GetStatusByName(string name);
    }
}
