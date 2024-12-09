using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Event
{
    public class ViewEventDto
    {
        [Required]
        public string EventName { get; set; } = string.Empty;
        public string? EventDescription { get; set; }
        [Required]
        public DateTimeOffset StartDate { get; set; }
        [Required]
        public DateTimeOffset EndDate { get; set; }
        [Required]
        public string Location { get; set; } = string.Empty;
        
    }
}
