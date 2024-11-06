# DailyPulse Solution

DailyPulse Solution is a .NET Core 6 application implementing Clean Architecture. It is designed to manage projects, employees, tasks, and locations in a structured and modular way. This repository contains a Web API for backend operations, organized using Clean Architecture principles.

## Table of Contents

- [Features](#features)
- [Architecture](#architecture)
- [Technologies](#technologies)
- [Installation](#installation)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [Database Migrations](#database-migrations)
- [Contributing](#contributing)
- [License](#license)

## Features

- **Admin Module**: Project creation, site creation, and employee data management.
- **Team Leader Module**: Employee and task assignment, task review, and confirmation.
- **Employee Module**: Task acceptance, work logging, task updates, and completion.
- **Self-Referencing Employee Hierarchy**: Employees can have a supervisor (team lead) using a self-referencing relationship.
- **Fluent API Configuration**: Leveraging Entity Framework Core Fluent API for precise model configuration.

## Architecture

This project follows **Clean Architecture** principles, splitting concerns across multiple layers:
1. **Core**:
   - **Domain**: Contains business entities, enums, and core logic.
   - **Application**: Contains use cases, business rules, interfaces, and application services.
2. **Infrastructure**:
   - Manages database access and persistence with Entity Framework Core.
3. **Presentation**:
   - Web API project, exposing endpoints to interact with the system.

## Technologies

- **.NET Core 5**
- **Entity Framework Core**
- **MySQL**
- **Fluent API for configuration**
- **Clean Architecture**

## Installation

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/yourusername/DailyPulseSolution.git
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
├── DailyPulse.Application        # Application layer for business logic and interfaces
├── DailyPulse.Infrastructure     # Infrastructure layer for EF Core configurations
└── DailyPulse.WebAPI             # Presentation layer, exposes the Web API
```

### Key Entities

- **Employee**: Represents an employee with properties like `Role`, `IsAdmin`, `IsTeamLeader`, and `ReportTo`.
- **Project**: Represents a project with navigation properties for `ScopeOfWork`, `Region`, `Location`, and `TeamLead`.
- **Task**: Represents a task, assigned to employees, with support for task logs and updates.

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

## License

This project is licensed under the MIT License.

---

Replace `yourusername` with your GitHub username if you plan to host it publicly. Let me know if you’d like further customization!
