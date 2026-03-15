![CI](https://github.com/AhmedIhab454/JobTracker.Api/actions/workflows/ci.yml/badge.svg)

# Job Application Tracker API

A secure REST API for tracking job applications, built with ASP.NET Core 8 and Entity Framework Core.
Users can register, log in, and manage their own job applications. Each application is private to the 
user who created it — no user can view or modify another user's data.

## Tech Stack

- **ASP.NET Core 8** — Web API framework
- **Entity Framework Core** — Database ORM
- **SQL Server** — Database
- **JWT Bearer Authentication** — Secure authentication
- **FluentValidation** — Input validation
- **Swagger / OpenAPI** — API documentation and testing

## Architecture

This project follows a layered architecture with clear separation of concerns:
```
Controllers → Services → Repositories → Database
```

- **Controllers** — handle HTTP requests and responses only
- **Services** — contain all business logic
- **Repositories** — handle all database access
- **DTOs** — control what data goes in and out of the API
- **Validators** — FluentValidation classes, separate from DTOs

## Features

- User registration and login with hashed passwords
- JWT token-based authentication
- Full CRUD operations for job applications
- User-scoped data — users only see their own applications
- Application status tracking — Applied, Interview, Offer, Rejected
- FluentValidation — validation rules separated from DTOs
- Global error handling — all errors return clean, consistent responses
- Layered architecture — controllers, services, repositories properly separated

## Testing

This project includes a unit test suite built with xUnit and Moq.

- Services are tested in complete isolation using mocked repositories
- No database required to run the tests
- 5 tests covering core business logic

To run the tests:
```bash
dotnet test
```

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or SQL Server LocalDB

### Setup

1. Clone the repository
```bash
   git clone https://github.com/AhmedIhab454/JobTracker.Api.git
```

2. Navigate to the project folder
```bash
   cd JobTracker.Api
```

3. Copy the example settings file and fill in your values
```bash
   cp appsettings.example.json appsettings.json
```

4. Update `appsettings.json` with your database connection string and a secure JWT secret key

5. Apply database migrations
```bash
   dotnet ef database update
```

6. Run the project
```bash
   dotnet run
```

7. Open Swagger UI at `https://localhost:{port}/swagger`

## API Endpoints

### Auth

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| POST | `/api/auth/register` | Register a new user | No |
| POST | `/api/auth/login` | Login and receive JWT token | No |

### Job Applications

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/jobapplication` | Get all applications for logged in user | Yes |
| GET | `/api/jobapplication/{id}` | Get a specific application by ID | Yes |
| POST | `/api/jobapplication` | Create a new job application | Yes |
| PUT | `/api/jobapplication/{id}` | Update an existing application | Yes |
| DELETE | `/api/jobapplication/{id}` | Delete an application | Yes |

## Application Status Flow
```
Applied → Interview → Offer
                   → Rejected
```

## Authentication

1. Register via `POST /api/auth/register`
2. Login via `POST /api/auth/login` and copy the token
3. In Swagger, click **Authorize** and paste your token
4. All protected endpoints are now accessible

## Security Notes

- Passwords are hashed using SHA256 before storage
- JWT tokens expire after 60 minutes
- All endpoints verify ownership — users can only access their own data
- Sensitive configuration values are stored outside of source control