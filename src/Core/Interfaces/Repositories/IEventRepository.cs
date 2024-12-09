using Core.Entities;


namespace Core.Interfaces.Repositories
{
    public interface IEventRepository:IBaseRepository<Event>
    {
        Task<Event?> GetEventWithParticipantsAsync(Guid eventId);
        Task<int> GetParticipantCountAsync(Guid eventId);

    }
}
