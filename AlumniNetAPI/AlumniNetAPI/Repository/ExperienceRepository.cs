using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;

namespace AlumniNetAPI.Repository
{
    public class ExperienceRepository : BaseRepository<Experience>, IExperienceRepository
    {
        public ExperienceRepository(AlumniNetAppContext context) : base(context)
        {
        }
    }
}
