using Core.Interfaces.Entities;

namespace Core.Entities
{
    public class Invitation: IBaseEntity, IInvitation
    {
        public Invitation() 
        {
            Id = Guid.NewGuid();   
        }
        public Guid Id { get; set; }
        public string Message { get; set; }=string.Empty;
        public bool IsAccepted { get; set; } = false;

        public Event Event { get; set; } = new();
        public Guid EventId { get; set; }

        public User Organizer { get; set; } = new();
        public Guid OrganizerId { get; set; }
        public User Receiver { get; set; } = new();
        public Guid ReceiverId { get; set; }
    }
}
