#EventAPI

EventAPI is a comprehensive platform designed for event management, allowing users to create, manage, and participate in events. The API supports robust functionalities, including user participation, invitation management, and event organization, making it an essential tool for collaborative event planning.

#Project Structure

This project is modular and adheres to clean architecture principles. Below is an overview of its structure:
- Core: Contains business logic and entity definitions.
- Infrastructure: Manages data access and external integrations.
- API: Provides endpoints and request handling.
- Tests: Includes unit and integration tests for ensuring functionality.

#Running the Project Locally
Follow these steps to set up and run the API locally:

#Prerequisites
- .NET 6 or later
- Docker (optional, for containerization)

#Steps
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

#Accessing the API

Once the API is running, visit:
```
http://localhost:5000/swagger/index.html
```
This URL provides an interactive Swagger documentation for testing all endpoints.


#Notes
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

