# EventAPI

EventAPI is a comprehensive platform designed for event management, allowing users to create, manage, and participate in events. The API supports robust functionalities, including user participation, invitation management, and event organization, making it an essential tool for collaborative event planning.

# Project Structure

This project is modular and adheres to clean architecture principles. Below is an overview of its structure:
- Core: Contains business logic and entity definitions.
- Infrastructure: Manages data access and external integrations.
- API: Provides endpoints and request handling.
- Tests: Includes unit and integration tests for ensuring functionality.

# API Methods
Below is a detailed list of the available service methods in the EventAPI project:

## Event Service
### CreateEventAsync
Creates a new event for the specified organizer.

### DeleteEventAsync
Deletes an event if the user is the organizer.

### UpdateEventAsync
Updates an existing event's details.

### GetAllEventsAsync
Retrieves all events based on a given filter.

### GetEventByIdAsync
Fetches the details of a specific event by its ID.

### GetAllEventsWithPaginationAsync
Returns paginated events with metadata.

### GetEventWithParticipantsAsync
Retrieves event details along with participants.

### GetEventListByDateRangeAsync
Fetches events that fall within a specific date range.

### GetOrganizedEventListForUserAsync
Lists all events organized by a specific user.

### GetParticipatedEventListForUserAsync
Lists events where the user is a participant.

### GetParticipantCountForEventAsync
Returns the count of participants for a specific event.

## Invitation Service
### GetReceivedInvitationsAsync
Lists invitations received by a specific user.

### GetSentInvitationsAsync
Lists invitations sent by a specific organizer.

### GetSingleInvitationAsync
Fetches details of a specific invitation by event ID and receiver ID.

### SendInvitationAsync
Sends invitations to multiple users for a specific event.

### ParticipateInvitationAsync
Allows a user to accept an invitation for an event.

## User Service
### IsUserValidAsync
Checks if a user exists in the system.

# Running the Project Locally
Follow these steps to set up and run the API locally:

# Prerequisites
- .NET 6 or later
- Docker (optional, for containerization)

# Steps
### 1- Clone the repository
```
 git clone https://github.com/secilseleci/EventAPI
```

### 2-Navigate to the Project Directory
```
cd EventAPI
```

### 3-Run Tests
```
dotnet test
```

### 4- Build the API
```
dotnet build
dotnet run --project API
```

# Accessing the API

Once the API is running, visit:
```
http://localhost:5000/swagger/index.html
```
This URL provides an interactive Swagger documentation for testing all endpoints.


# Notes
- The API uses [your database technology, e.g., SQL Server, PostgreSQL, etc.]. Ensure the connection string in appsettings.json is configured correctly for your local or remote environment.
- Customize configurations in appsettings.json if needed (e.g., connection strings, logging).
```
{
    "ConnectionStrings": {
        "DefaultConnection": "Server=your-server;Database=EventAPI;User Id=your-user;Password=your-password;"
    }
}
```
-Run database migrations to set up the schema:
```
dotnet ef database update
```
- Use Postman or Swagger to test endpoints directly.

