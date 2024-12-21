using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public async Task<PaginationDto<Event>?> GetAllEventsWithPaginationAsync(
     int page, int pageSize )
        {
            var totalCount = await _dbContext.Events.CountAsync();

            var paginatedEvents = await _dbContext.Events
                .OrderByDescending(e => e.StartDate) // En son oluşturulan etkinlikler için
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginationDto<Event>
            {
                Data = paginatedEvents,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }


    }
}
