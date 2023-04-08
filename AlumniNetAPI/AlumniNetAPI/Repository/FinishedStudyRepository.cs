using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlumniNetAPI.Repository
{
    public class FinishedStudyRepository : BaseRepository<FinishedStudy>, IFinishedStudyRepository
    {
        public FinishedStudyRepository(AlumniNetAppContext context) : base(context)
        {
        }

        public async Task<FinishedStudy> GetFinishedStudyByIdAsync(int id)
        {
            FinishedStudy study = await _dbSet.SingleAsync(s => s.FinishedStudyId == id);
            return study;
        }

        public async Task<FinishedStudy> GetFinishedStudyByProfileIdAsync(int id)
        {
            FinishedStudy study = await _dbSet.SingleAsync(s => s.ProfileId == id);
            return study;
        }

    }
}
