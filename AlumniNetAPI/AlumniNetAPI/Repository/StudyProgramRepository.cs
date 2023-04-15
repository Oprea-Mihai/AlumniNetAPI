using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlumniNetAPI.Repository
{
    public class StudyProgramRepository : BaseRepository<StudyProgram>, IStudyProgramRepository
    {
        public StudyProgramRepository(AlumniNetAppContext context) : base(context)
        {
        }
        public async Task<StudyProgram> GetStudyProgramByIdAsync(int id)
        {
            StudyProgram studyProgram = await _dbSet.SingleAsync(s => s.StudyProgramId == id);
            return studyProgram;
        }
    }
}
