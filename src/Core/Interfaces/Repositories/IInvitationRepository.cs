using Core.Entities;
using Core.Utilities.Results;
using System.Linq.Expressions;


namespace Core.Interfaces.Repositories
{
    public interface IInvitationRepository:IBaseRepository<Invitation>
    {
        Task<Invitation?> GetInvitationByEventAndReceiverAsync(Guid eventId, Guid receiverId);
        Task<List<Invitation>?> GetInvitationsWithDetailsAsync(Expression<Func<Invitation, bool>> predicate);

    }
}
