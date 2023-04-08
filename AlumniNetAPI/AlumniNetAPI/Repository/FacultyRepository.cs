using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlumniNetAPI.Repository
{
    public class FacultyRepository : BaseRepository<Faculty>, IFacultyRepository
    {
        public FacultyRepository(AlumniNetAppContext context) : base(context)
        {
        }

        public async Task<Faculty> GetFacultyByIdAsync(int id)
        {
            Faculty faculty = await _dbSet.SingleAsync(f => f.FacultyId== id);
            return faculty;
        }
    }
}
