using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;

namespace AlumniNetAPI.Repository
{
    public class SpecializationRepository : BaseRepository<Specialization>, ISpecializationRepository
    {
        public SpecializationRepository(AlumniNetAppContext context) : base(context)
        {
        }
    }
}
