using AlumniNetAPI.Models;

namespace AlumniNetAPI.Repository.Interfaces
{
    public interface IEventRepository : IBaseRepository<Event>
    {
        Task<Event> GetEventByIdAsync(int id);
    }
}
