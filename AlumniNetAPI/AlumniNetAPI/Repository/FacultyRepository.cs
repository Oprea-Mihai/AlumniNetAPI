using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;

namespace AlumniNetAPI.Repository
{
    public class FacultyRepository : BaseRepository<Faculty>, IFacultyRepository
    {
        public FacultyRepository(AlumniNetAppContext context) : base(context)
        {
        }
    }
}
