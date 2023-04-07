using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;

namespace AlumniNetAPI.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AlumniNetAppContext context) : base(context)
        {
        }
    }
}
