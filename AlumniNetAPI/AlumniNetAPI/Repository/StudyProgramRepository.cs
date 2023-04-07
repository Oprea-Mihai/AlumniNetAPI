using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;

namespace AlumniNetAPI.Repository
{
    public class StudyProgramRepository : BaseRepository<StudyProgram>, IStudyProgramRepository
    {
        public StudyProgramRepository(AlumniNetAppContext context) : base(context)
        {
        }
    }
}
