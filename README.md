# Company Management System

An ASP.NET Core MVC application for managing employees and departments, organized into presentation, business-logic, and data-access projects.

## Features

- Employee listing and search.
- Employee create and update workflows.
- Employee active/inactive state toggling.
- Department management workflows.
- View models for create, update, and display scenarios.
- AutoMapper-based mapping between application models and data entities.
- Repository abstractions and EF Core data access.
- Specification-based employee queries.
- SQL Server and EF Core migrations.

## Tech Stack

- C#
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- AutoMapper
- Dependency Injection

## Architecture

```text
CompanyManagementSystem/
  MVC host, controllers, Razor views, dependency registration
          |
          v
CompanyManagementMVC.BLL/
  Services, interfaces, view models, mapping, query specifications
          |
          v
CompanyManagementMVC.DAL/
  Entities, DbContext, repository interfaces and implementations
          |
          v
      SQL Server
```

### Project responsibilities

| Project | Responsibility |
|---|---|
| `CompanyManagementSystem` | MVC presentation layer and application startup |
| `CompanyManagementMVC.BLL` | Business services, contracts, view models, and mapping |
| `CompanyManagementMVC.DAL` | Entities, EF Core context, repositories, and persistence |

## Prerequisites

- A compatible .NET SDK for the target framework in the project files
- SQL Server
- .NET EF Core CLI tool if you need to manage migrations

## Configure the Database

The application reads `ConnectionStrings:DefaultConnection`. Set the connection string for your own SQL Server environment with User Secrets instead of committing machine-specific details:

```bash
dotnet user-secrets init --project CompanyManagementSystem/CompanyManagementSystem.PL.csproj
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "YOUR_SQL_SERVER_CONNECTION_STRING" --project CompanyManagementSystem/CompanyManagementSystem.PL.csproj
```

## Run Locally

From the repository root:

```bash
dotnet restore "3 Tre MVC.sln"
dotnet run --project CompanyManagementSystem/CompanyManagementSystem.PL.csproj
```

The application attempts to apply pending EF Core migrations during startup, so ensure the database connection is valid before launching it.

## Learning Focus

This project demonstrates layered application design, repository abstractions, business services, view-model mapping, dependency injection, and MVC CRUD workflows.
