using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Constants
{
    public class Messages
    {
        #region Event Messages

        public const string EventNotFound = "Event not found";
        public const string EmptyEventList = "Event list is empty with the current filter";
        public const string CreateEventSuccess = "Event created successfully";
        public const string CreateEventError = "Error occured while registering the Event to Database";
        public const string UpdateEventSuccess = "Event updated successfully";
        public const string UpdateEventError = "Error occured while updating the Event in Database";
        public const string DeleteEventSuccess = "Event deleted succesfully";
        public const string DeleteEventError = "Error occured while deleting the Event in Database";
        public const string EventsRetrievedSuccessfully = "Events retrieved successfully";
        #endregion

        #region  Invitation Messages

        public const string InvitationNotFound = "Invitation not found";
        public const string EmptyInvitationList = "Invitation list is empty with the current filter";
        public const string CreateInvitationSuccess = "Invitation created successfully";
        public const string CreateInvitationError = "Error occured while registering the Invitation to Database";
        public const string UpdateInvitationSuccess = "Invitation updated successfully";
        public const string UpdateInvitationError = "Error occured while updating the Invitation in Database";
        public const string DeleteInvitationSuccess = "Invitation deleted succesfully";
        public const string DeleteInvitationError = "Error occured while deleting the Invitation in Database";
        public const string InvitationsRetrievedSuccessfully = "Invitations retrieved successfully";
        public const string InvitationsSentSuccessfully = "Invitations sent successfully";
        public const string AllUsersAlreadyInvited = "All users already invited";
        #endregion

        #region  Participant Messages

        public const string ParticipantNotFound = "Participant not found";
        public const string EmptyParticipantList = "Participant list is empty with the current filter";
        public const string CreateParticipantSuccess = "Participant created successfully";
        public const string CreateParticipantError = "Error occured while registering the Participant to Database";
        public const string UpdateParticipantSuccess = "Participant updated successfully";
        public const string UpdateParticipantError = "Error occured while updating the Participant in Database";
        public const string DeleteParticipantSuccess = "Participant deleted succesfully";
        public const string DeleteParticipantError = "Error occured while deleting the Participant in Database";
        public const string ParticipantCountRetrievedSuccessfully = "Participant count retrieved successfully.";

        #endregion

        public const string UserNotFound = "User not found"; // Kimlik doğrulama veya kontrol sırasında
        public const string UnauthorizedAccess = "Unauthorized access"; // Yetkilendirme hataları için

    }
}
