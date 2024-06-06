using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlumniNetAPI.Repository
{
    public class StatusRepostitory : BaseRepository<Status>, IStatusRepository
    {
        public StatusRepostitory(AlumniNetAppContext context) : base(context)
        {
        }

        public async Task<Status> GetStatusById(int id)
        {
            return await _dbSet.SingleAsync(s => s.StatusId == id);
        }

        public async Task<Status> GetStatusByName(string name)
        {
            return await _dbSet.Where(s => s.StatusName.Contains(name)).SingleAsync();
        }
    }
}
