using Core.DTOs;
using Core.Entities;
using System.Linq.Expressions;


namespace Core.Interfaces.Repositories
{
    public interface IEventRepository:IBaseRepository<Event>
    {
        Task<PaginationDto<Event>?> GetAllEventsWithPaginationAsync(int page, int pageSize);
        Task<Event?> GetEventWithParticipantsAsync(Guid eventId);
        Task<int> GetParticipantCountAsync(Guid eventId);

    }
}
