using AlumniNetAPI.Models;

namespace AlumniNetAPI.Repository.Interfaces
{
    public interface IStudyProgramRepository:IBaseRepository<StudyProgram>
    {
        Task<StudyProgram> GetStudyProgramByIdAsync(int id);
    }
}
