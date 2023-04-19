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
            FinishedStudy study = await _dbSet.Include(fs => fs.LearningSchedule)
        .Include(fs => fs.Profile)
        .Include(fs => fs.Specialization).ThenInclude(s => s.Faculty)
        .Include(fs => fs.StudyProgram).SingleAsync(s => s.FinishedStudyId == id);
            return study;
        }

        public async Task<List<FinishedStudy>> GetAllDetailed()
        {
           List<FinishedStudy> study = await _dbSet.Include(fs => fs.LearningSchedule)
      .Include(fs => fs.Profile)
      .Include(fs => fs.Specialization).ThenInclude(s => s.Faculty)
      .Include(fs => fs.StudyProgram).ToListAsync();
            return study;
        }

        public async Task<FinishedStudy> GetFinishedStudyByProfileIdAsync(int id)
        {
            FinishedStudy study = await _dbSet.Include(fs => fs.LearningSchedule)
        .Include(fs => fs.Profile)
        .Include(fs => fs.Specialization)
        .Include(fs => fs.StudyProgram).SingleAsync(s => s.ProfileId == id);
            return study;
        }

    }
}
