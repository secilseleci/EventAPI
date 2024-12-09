
using Core.Interfaces.Entities;

namespace Core.Entities
{
    public class User:IBaseEntity,IUser
    {
        public User() 
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string FullName { get; set; }=string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

        public List<Event> OrganizedEvents { get; set; } = new();
        public List<Participant> ParticipatedEvents { get; set; } = new();
        public List<Invitation> SentInvitations { get; set; }= new ();
        public List<Invitation> ReceivedInvitations { get; set; } = new();
    }
}
