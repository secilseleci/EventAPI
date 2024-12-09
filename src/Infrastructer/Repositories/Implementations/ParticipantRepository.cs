using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories.Implementations
{
    public class ParticipantRepository(EventApiDbContext context) : BaseRepository<Participant>(context), IParticipantRepository
    {
    }
}
