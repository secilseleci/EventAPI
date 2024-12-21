using Core.DTOs;
using Core.DTOs.Event;
using Core.Entities;
using Core.Utilities.Results;
using System.Linq.Expressions;

namespace Core.Interfaces.Services
{
    public interface IEventService
    {
        Task<IResult> CreateEventAsync(CreateEventDto createEventDto,Guid userId, CancellationToken cancellationToken);
        Task<IResult> DeleteEventAsync(Guid eventId,Guid userId, CancellationToken cancellationToken);
        Task<IResult> UpdateEventAsync(UpdateEventDto updateEventDto,Guid userId, CancellationToken cancellationToken);
        
        Task<IDataResult<ViewEventDto>> GetEventByIdAsync(Guid eventId, CancellationToken cancellationToken);
        Task<IDataResult<PaginationDto<ViewEventDto>>> GetAllEventsWithPaginationAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task<IDataResult<IEnumerable<ViewEventDto>>> GetAllEventsAsync(Expression<Func<Event, bool>> predicate, CancellationToken cancellationToken);
        
        Task<IDataResult<ViewEventWithParticipantsDto>> GetEventWithParticipantsAsync(Guid eventId, CancellationToken cancellationToken);
        Task<IDataResult<IEnumerable<ViewEventDto>>> GetOrganizedEventListForUserAsync(Guid userId, CancellationToken cancellationToken);
        Task<IDataResult<IEnumerable<ViewEventDto>>> GetParticipatedEventListForUserAsync(Guid userId, CancellationToken cancellationToken);
        Task<IDataResult<IEnumerable<ViewEventDto>>> GetEventListByDateRangeAsync(DateTimeOffset startDate, DateTimeOffset endDate, CancellationToken cancellationToken);
        Task<IDataResult<int>> GetParticipantCountForEventAsync(Guid eventId, CancellationToken cancellationToken);

    }
}
