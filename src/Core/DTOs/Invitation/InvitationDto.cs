namespace Core.DTOs.Invitation
{
    public class InvitationDto
    {
        public Guid InvitationId { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool? IsAccepted { get; set; } = false;

        public string EventName { get; set; } = string.Empty;
        public DateTimeOffset EventStartDate { get; set; }  
        public DateTimeOffset EventEndDate { get; set; }  
        public string Timezone { get; set; } = string.Empty;
        public string Organizer { get; set; } = string.Empty;
        public string Receiver { get; set; } = string.Empty;


    }
}
