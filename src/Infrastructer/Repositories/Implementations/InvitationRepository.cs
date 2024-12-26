using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Utilities.Results;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Implementations
{
    public class InvitationRepository(EventApiDbContext context) : BaseRepository<Invitation>(context), IInvitationRepository
    {
        public async Task<Invitation?> GetInvitationByEventAndReceiverAsync(Guid eventId, Guid receiverId)
        {
            return await _dbContext.Invitations
                .Include(i => i.Event)
                .Include(i => i.Organizer)
                .Include(i => i.Receiver)
                .FirstOrDefaultAsync(i => i.EventId == eventId && i.ReceiverId == receiverId);
        }

        public async Task<List<Invitation>?> GetInvitationsWithDetailsAsync(Expression<Func<Invitation, bool>> predicate)
        {
            return await _dbContext.Invitations
                .Include(i => i.Event)  
                .Include(i => i.Organizer)
                .Include(i => i.Receiver)
                .Where(predicate)
                .ToListAsync();
        }
    }
}
