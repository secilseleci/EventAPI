using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class EventRepository(EventApiDbContext context) : BaseRepository<Event>(context), IEventRepository
    {
        public async Task<Event?> GetEventWithParticipantsAsync(Guid eventId)
        {
            var eventEntity=await _dbContext.Events
            .Where(e=>e.Id==eventId)
            .Include(c=> c.Participants)
            .FirstOrDefaultAsync();
             
            return eventEntity;
    }

        public async Task<int> GetParticipantCountAsync(Guid eventId)
        {
            return await _dbContext.Participants
                .CountAsync(p => p.EventId == eventId);
         }
    }
}
