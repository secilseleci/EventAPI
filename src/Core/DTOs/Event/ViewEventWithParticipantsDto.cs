using Core.DTOs.Participant;

namespace Core.DTOs.Event
{
    public class ViewEventWithParticipantsDto
    {
        public Guid Id { get; set; } // Event ID
        public string EventName { get; set; } = string.Empty;
        public string? EventDescription { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Timezone { get; set; } = "UTC";

        // Katılımcı bilgileri
        public List<ParticipantDto> Participants { get; set; } = new();
    }
}
