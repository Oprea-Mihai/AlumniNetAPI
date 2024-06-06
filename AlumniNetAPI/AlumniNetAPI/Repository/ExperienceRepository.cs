using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlumniNetAPI.Repository
{
    public class ExperienceRepository : BaseRepository<Experience>, IExperienceRepository
    {
        public ExperienceRepository(AlumniNetAppContext context) : base(context)
        {
        }

        public async Task<Experience> GetExperienceByIdAsync(int id)
        {
            Experience experience=await _dbSet.SingleAsync(e=>e.ExperienceId==id);
            return experience;
        }
    }
}
