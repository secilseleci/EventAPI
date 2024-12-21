using Core.Entities;
using Core.Utilities.Results;


namespace Core.Interfaces.Repositories
{
    public interface IInvitationRepository:IBaseRepository<Invitation>
    {
        Task<Invitation?> GetInvitationByEventAndReceiverAsync(Guid eventId, Guid receiverId);

    }
}
