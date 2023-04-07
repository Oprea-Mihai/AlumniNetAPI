using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;

namespace AlumniNetAPI.Repository
{
    public class LearningScheduleRepository : BaseRepository<LearningSchedule>, ILearningScheduleRepository
    {
        public LearningScheduleRepository(AlumniNetAppContext context) : base(context)
        {
        }
    }
}
