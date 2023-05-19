using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace AlumniNetAPI.Repository
{
    public class InvitedUserRepository : BaseRepository<InvitedUser>, IInvitedUserRepository
    {
        public InvitedUserRepository(AlumniNetAppContext context) : base(context)
        {
        }

        public async Task<InvitedUser> GetInvitedUserById(int id)
        {
            return await _dbSet.Where(i=>i.InvitedUserId==id).SingleAsync();
        }

    }
}
