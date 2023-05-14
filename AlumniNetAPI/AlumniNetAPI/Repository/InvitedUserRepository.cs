using AlumniNetAPI.Models;
using AlumniNetAPI.Repository.Interfaces;

namespace AlumniNetAPI.Repository
{
    public class InvitedUserRepository : BaseRepository<InvitedUser>, IInvitedUserRepository
    {
        public InvitedUserRepository(AlumniNetAppContext context) : base(context)
        {
        }
    }
}
