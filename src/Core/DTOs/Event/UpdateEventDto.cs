using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Event
{
    public class UpdateEventDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
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
