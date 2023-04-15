using AlumniNetAPI.Models;

namespace AlumniNetAPI.Repository.Interfaces
{
    public interface ILearningScheduleRepository:IBaseRepository<LearningSchedule>
    {
        Task<LearningSchedule> GetLearningScheduleByIdAsync(int id);
    }
}
