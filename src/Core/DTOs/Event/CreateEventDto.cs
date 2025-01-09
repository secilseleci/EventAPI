using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Event
{
    public class CreateEventDto
    {
        [Required]
        [MinLength(5)]
        public string EventName { get; set; } = string.Empty;
        public string? EventDescription { get; set; }
        [Required]
        [ValidDateRange] 
        public DateTimeOffset StartDate { get; set; }
        [Required]
        public DateTimeOffset EndDate { get; set; }
        [Required]
        public string Location { get; set; } = string.Empty;
        [Required]
        public string Timezone { get; set; } = "UTC"; // Varsayılan "UTC"
    }
}
