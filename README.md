# Employee Management API

## Overview

This project is a RESTful API built with ASP.NET Core Web API to manage employee records. It follows a clean architecture by separating the application into Controllers, Services, Repositories, Models, DTOs, Middleware, and Data layers.

## Features

- Create Employee
- Get All Employees
- Get Employee By ID
- Update Employee
- Soft Delete Employee
- Search by Name or Department
- Pagination
- Sorting by Name or Hire Date
- Global Exception Handling
- Logging
- Swagger / OpenAPI

## Technologies

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI

## Project Structure

```
Controllers/
Services/
Repositories/
Models/
DTOs/
Data/
Middleware/
Program.cs
appsettings.json
```

## Setup Instructions

1. Clone the repository.
2. Open the project in Visual Studio or Visual Studio Code.
3. Restore the NuGet packages.
4. Update the SQL Server connection string in `appsettings.json`.
5. Run the application.
6. Open Swagger to test the API endpoints.

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/employees` | Create a new employee |
| GET | `/api/employees` | Get all employees |
| GET | `/api/employees/{id}` | Get an employee by ID |
| PUT | `/api/employees/{id}` | Update an employee |
| DELETE | `/api/employees/{id}` | Soft delete an employee |
| GET | `/api/employees/search` | Search by name or department |

### Example Request

```json
POST /api/employees

{
  "fullName": "John Smith",
  "email": "john@example.com",
  "department": "IT",
  "salary": 5000,
  "hireDate": "2024-01-15"
}
```

## Validation Rules

- FullName is required.
- Email must be a valid email address.
- Email must be unique.
- Salary must be greater than zero.

## Assumptions

- Employees are soft deleted by setting `IsActive` to `false`.
- Only active employees are returned when retrieving employee records.
- Email addresses must be unique.
- Default pagination uses `page = 1` and `pageSize = 10`.

## Notes

During development, the machine had insufficient disk space, which affected local testing:

- SQL Server could not be started locally because of the disk space limitation.
- The optional JWT Authentication package (`Microsoft.AspNetCore.Authentication.JwtBearer`) could not be installed because NuGet was unable to download the package due to insufficient disk space.
- All required project features and the required API endpoints were completed successfully.
- Swagger/OpenAPI was implemented and is available for testing the API.
