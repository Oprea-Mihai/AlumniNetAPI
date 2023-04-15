using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlumniNetAPI.Repository
{
    public class LearningScheduleRepository : BaseRepository<LearningSchedule>, ILearningScheduleRepository
    {
        public LearningScheduleRepository(AlumniNetAppContext context) : base(context)
        {
        }

        public async Task<LearningSchedule> GetLearningScheduleByIdAsync(int id)
        {
            LearningSchedule learningSchedule = await _dbSet.SingleAsync(l => l.LearningScheduleId == id);
            return learningSchedule;
        }
    }
}
