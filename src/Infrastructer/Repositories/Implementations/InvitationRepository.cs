using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Utilities.Results;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories.Implementations
{
    public class InvitationRepository(EventApiDbContext context) : BaseRepository<Invitation>(context), IInvitationRepository
    {
      
    }
}
