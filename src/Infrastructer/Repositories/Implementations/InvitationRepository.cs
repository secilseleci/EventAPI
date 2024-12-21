using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Utilities.Results;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class InvitationRepository(EventApiDbContext context) : BaseRepository<Invitation>(context), IInvitationRepository
    {
        public async Task<Invitation?> GetInvitationByEventAndReceiverAsync(Guid eventId, Guid receiverId)
        {
            return await _dbContext.Invitations
                    .FirstOrDefaultAsync(i => i.EventId == eventId && i.ReceiverId == receiverId);
        }
    }
}
