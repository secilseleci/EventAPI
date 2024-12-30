using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Event
{
    public class CreateEventDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "EventName must be between 3 and 50 characters.")]
        public string EventName { get; set; } = string.Empty;
        public string? EventDescription { get; set; }
        [Required]
        public DateTimeOffset StartDate { get; set; }
        [Required]
        public DateTimeOffset EndDate { get; set; }
        [Required]
        public string Location { get; set; } = string.Empty;
        [Required]
        public string Timezone { get; set; } = "UTC"; // Varsayılan "UTC"
    }
}
