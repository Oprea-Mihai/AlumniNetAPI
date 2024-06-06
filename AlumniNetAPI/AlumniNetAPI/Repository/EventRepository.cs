using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlumniNetAPI.Repository
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(AlumniNetAppContext context) : base(context)
        {
        }
            public async Task<Event> GetEventByIdAsync(int id)
            {
                Event eventResult = await _dbSet.SingleAsync(e=> e.EventId == id);
                return eventResult;
            }
    }
}
