using AlumniNetAPI.Models;

namespace AlumniNetAPI.Repository.Interfaces
{
    public interface IFinishedStudyRepository : IBaseRepository<FinishedStudy>
    {
        Task<FinishedStudy> GetFinishedStudyByIdAsync(int id);
        Task<FinishedStudy> GetFinishedStudyByProfileIdAsync(int id);
    }
}
