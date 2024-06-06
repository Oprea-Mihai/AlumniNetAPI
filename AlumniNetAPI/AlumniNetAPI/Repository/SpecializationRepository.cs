using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlumniNetAPI.Repository
{
    public class SpecializationRepository : BaseRepository<Specialization>, ISpecializationRepository
    {
        public SpecializationRepository(AlumniNetAppContext context) : base(context)
        {
        }
        public async Task<Specialization> GetSpecializationByIdAsync(int id)
        {
            Specialization specialization = await _dbSet.SingleAsync(s => s.SpecializationId == id);
            return specialization;
        }

        public async Task<List<Specialization>> GetSpecializationsByFacultyIdAsync(int facultyId)
        {
            List<Specialization> specializations = await _dbSet.Where(s => s.FacultyId == facultyId).ToListAsync();
            return specializations;
        }
    }
}
