using Core.Interfaces.Entities;
using System.ComponentModel.DataAnnotations;

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

        public Event Event { get; set; } = null!;
        public Guid EventId { get; set; }

        public User Organizer { get; set; } = null!;
        public Guid OrganizerId { get; set; }

        public User Receiver { get; set; } = null!;
        public Guid ReceiverId { get; set; }
    }
}
