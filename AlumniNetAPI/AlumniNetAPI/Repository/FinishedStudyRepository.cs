using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;

namespace AlumniNetAPI.Repository
{
    public class FinishedStudyRepository : BaseRepository<FinishedStudy>, IFinishedStudyRepository
    {
        public FinishedStudyRepository(AlumniNetAppContext context) : base(context)
        {
        }
    }
}
