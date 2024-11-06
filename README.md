# DailyPulse Solution

DailyPulse Solution is a .NET Core 6 application implementing **Clean Architecture** along with **CQRS** and **Mediator** patterns. The project is designed to manage projects, employees, tasks, and locations in a modular and organized way.

## Table of Contents

- [Features](#features)
- [Architecture](#architecture)
- [Technologies](#technologies)
- [Installation](#installation)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [Database Migrations](#database-migrations)
- [Contributing](#contributing)

## Features

- **Admin Module**: Project creation, site creation, and employee data management.
- **Team Leader Module**: Employee and task assignment, task review, and confirmation.
- **Employee Module**: Task acceptance, work logging, task updates, and completion.
- **Self-Referencing Employee Hierarchy**: Employees can have a supervisor (team lead) using a self-referencing relationship.
- **CQRS and Mediator**: Implements Command and Query responsibilities separated through **MediatR** for improved organization and scalability.
- **Fluent API Configuration**: Leveraging Entity Framework Core Fluent API for precise model configuration.

## Architecture

This project follows **Clean Architecture** principles, CQRS, and Mediator patterns, creating a modular separation of concerns:
1. **Core**:
   - **Domain**: Contains business entities, enums, and core logic.
   - **Application**: Contains use cases, business rules, commands, queries, and application services.
2. **Infrastructure**:
   - Manages database access and persistence with Entity Framework Core.
3. **Presentation**:
   - Web API project, exposing endpoints to interact with the system.

### CQRS and Mediator

The **CQRS** (Command Query Responsibility Segregation) pattern is applied by separating read and write operations, improving scalability and clarity:
- **Commands**: Used for actions that change data (create, update, delete).
- **Queries**: Used for retrieving data without altering it.

The **Mediator** pattern, implemented with **MediatR**, enables decoupling of command/query handlers from controllers, promoting a single entry point for each request.

## Technologies

- **.NET Core 6**
- **Entity Framework Core**
- **MySQL**
- **Fluent API for configuration**
- **Clean Architecture**
- **CQRS with MediatR**

## Installation

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/abdelrahmanalimohamed/DailyPulseSolution.git
   cd DailyPulseSolution
   ```

2. **Set Up Database**:
   Ensure you have MySQL installed and configure the connection string in `appsettings.json` in the `Presentation` project:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=your_server;Database=DailyPulseDb;User=your_user;Password=your_password;"
   }
   ```

3. **Restore Packages**:
   Restore the NuGet packages by running:
   ```bash
   dotnet restore
   ```

4. **Apply Migrations**:
   Apply the migrations to set up the database schema:
   ```bash
   dotnet ef database update --project DailyPulse.Infrastructure --startup-project DailyPulse.WebAPI
   ```

## Usage

Run the application from the `Presentation` layer:

```bash
dotnet run --project DailyPulse.WebAPI
```

The API will be accessible at `http://localhost:5000` or `https://localhost:5001` for secure connections.

### API Endpoints

- **/api/employees**: Manage employees, including team leads and reports.
- **/api/projects**: Manage projects, including assignment of team leads and employees.
- **/api/tasks**: Manage tasks, task acceptance, and work logs.

## Project Structure

```
DailyPulseSolution
├── DailyPulse.Domain             # Contains core domain entities and enums
├── DailyPulse.Application        # Application layer with CQRS command/query handlers
│   ├── Commands                  # Command handlers for creating, updating, and deleting data
│   └── Queries                   # Query handlers for reading data
├── DailyPulse.Infrastructure     # Infrastructure layer for EF Core configurations and MediatR
└── DailyPulse.WebAPI             # Presentation layer, exposes the Web API
```

### Key Entities

- **Employee**: Represents an employee with properties like `Role`, `IsAdmin`, `IsTeamLeader`, and `ReportTo`.
- **Project**: Represents a project with navigation properties for `ScopeOfWork`, `Region`, `Location`, and `TeamLead`.
- **Task**: Represents a task, assigned to employees, with support for task logs and updates.

### CQRS Commands and Queries

- **Commands**:
  - `CreateEmployeeCommand`
  - `UpdateProjectCommand`
  - `DeleteTaskCommand`
- **Queries**:
  - `GetEmployeeDetailsQuery`
  - `GetProjectListQuery`
  - `GetTaskByIdQuery`

### Database Migrations

To add or remove migrations, use the following commands:

- **Add a Migration**:
  ```bash
  dotnet ef migrations add <MigrationName> --project DailyPulse.Infrastructure --startup-project DailyPulse.WebAPI
  ```

- **Remove a Migration**:
  ```bash
  dotnet ef migrations remove --project DailyPulse.Infrastructure --startup-project DailyPulse.WebAPI
  ```

## Contributing

Feel free to submit issues, fork the repository, and make pull requests. For major changes, please open an issue first to discuss what you would like to change.

---

Let me know if you'd like any further customization.
