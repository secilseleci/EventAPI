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
        public User User { get; set; } = new();
        public Guid EventId { get; set; }
        public Event Event { get; set; } = new();


    }
}
