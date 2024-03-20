# Event Scheduler API Project Structure

## Project Overview

Event Scheduler API
The Event Scheduler API is a RESTful backend application for scheduling and managing events. It provides endpoints for creating, viewing, updating, and deleting events, along with basic user registration and authentication.

### Features

- Event Management: CRUD operations for events (Create, Read, Update, Delete).
- User Authentication: Endpoints for user registration and login, with authentication for event-related endpoints.
- Documentation: Clear API documentation including request and response formats.

### Tech Stack

- Framework: ASP.NET Core
- Database: Entity Framework Core (for ORM)
- Authentication: JWT (JSON Web Tokens)
- Documentation: Swagger

### Swagger Preview

Access the Swagger documentation [here](https://dg-event-scheduler-api.azurewebsites.net/swagger/index.html).

## Folder Structure

````plaintext
- .gitignore
- appsettings.Development.json
- appsettings.json
- Data/
  - Models/
  - Repository/
- event-scheduler.api.csproj
- event-scheduler.api.sln
- Exceptions/
  - GlobalExceptionHandler.cs
- Extensions/
  - ServiceExtensions.cs
- Features/
  - Auth/
  - Event/
  - User/
- Mapping/
  - GlobalResponse.cs
  - Patcher.cs
- Program.cs


## Setup Instructions

```bash
# Clone the repository
git clone <repository-url>

# Navigate to the project directory
cd event-scheduler-api

# Set up the database
# Update connection string in appsettings.json
# Run Entity Framework migrations
dotnet ef database update

# Run the application
dotnet run
````
