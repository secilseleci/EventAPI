using Core.Interfaces.Entities;

namespace Core.Entities
{
    public class Event : IBaseEntity, IEvent
    {        
        public Event() 
            {
                Id = Guid.NewGuid();
            }
        
        public Guid Id { get; set; }

        public string EventName { get; set; } = string.Empty;
        public string? EventDescription { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Timezone { get; set; } = "UTC";//global timezone

        public string Location { get; set; } = string.Empty;

        public Guid OrganizerId { get; set; }
        public User Organizer { get; set; } = null!;
        public List<Participant> Participants { get; set; } = new();
        public List<Invitation> Invitations { get; set; } = new();
    }
}
