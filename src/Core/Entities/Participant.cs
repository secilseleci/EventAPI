using  Core.Interfaces.Entities;

namespace Core.Entities
{
    public class Participant:IBaseEntity 
    {
        public Participant() 
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public User User { get; set; } = null!;
        public Guid UserId { get; set; }

        public Guid EventId { get; set; }
        public Event Event { get; set; } = null!;


    }
}
