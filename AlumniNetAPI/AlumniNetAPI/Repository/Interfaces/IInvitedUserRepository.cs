using AlumniNetAPI.Models;

namespace AlumniNetAPI.Repository.Interfaces
{
    public interface IInvitedUserRepository : IBaseRepository<InvitedUser>
    {
        Task<InvitedUser> GetInvitedUserById(int id);
        Task<List<InvitedUser>> GetInvitationsByEventId(int id);
    }
}
