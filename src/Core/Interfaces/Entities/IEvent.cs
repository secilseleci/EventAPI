 

namespace Core.Interfaces.Entities
{
    public interface IEvent
    {
        public string EventName { get; set; }  
        public string? EventDescription { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Timezone { get; set; }  

        public string Location { get; set; } 
    }
}
