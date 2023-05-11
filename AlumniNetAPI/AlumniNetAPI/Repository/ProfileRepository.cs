using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlumniNetAPI.Repository
{
    public class ProfileRepository : BaseRepository<Profile>, IProfileRepository
    {
        public ProfileRepository(AlumniNetAppContext context) : base(context)
        {
        }
        public async Task<Profile> GetProfileByIdAsync(int id)
        {
            Profile profile = await _dbSet.SingleAsync(p => p.ProfileId == id);
            return profile;
        }

        public async Task<Profile> GetProfileWithStudiesByIdAsync(int id)
        {
            Profile profile = await _dbSet.Include(fs=>fs.FinishedStudies)
                .ThenInclude(s => s.Specialization).ThenInclude(f => f.Faculty)
                .SingleAsync(p => p.ProfileId == id);
            return profile;
        }


    }
}
