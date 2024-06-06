using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace AlumniNetAPI.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AlumniNetAppContext context) : base(context)
        {
        }

        public async Task<User>GetUserByIdAsync(string id)
        {
            User user = await _dbSet.SingleAsync(u=>u.UserId==id);
            return user;
        }

        public async Task<User> GetUserWithProfileByIdAsync(string id)
        {
            User user = await _dbSet.Include(u=>u.Profile).SingleAsync(u => u.UserId == id);
            return user;
        }

        public async Task<User> GetUserByProfileIdAsync(int id)
        {
            User user = await _dbSet.SingleAsync(u => u.ProfileId == id);
            return user;
        }

       
    }
}
