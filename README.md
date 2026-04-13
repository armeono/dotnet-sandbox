# dotnet-practice

This repository is a personal .NET sandbox project for learning by building.

It is used to practice:
- ASP.NET Core minimal APIs
- dependency injection
- Entity Framework Core with SQLite
- DTOs and endpoint design
- migrations and model configuration
- unit and integration testing with xUnit

The project is intentionally small and iterative. The goal is not to build a production app quickly, but to improve .NET fundamentals and engineering habits one lesson at a time.

## Current app

The app is a simple game store API with:
- `games` endpoints
- `genres` endpoints
- EF Core data access
- SQLite persistence
- a separate test project for learning testing patterns

## Run the app

From the repo root:

```bash
dotnet run --project dotnet-practice/dotnet-practice.csproj
```

## Run tests

From the repo root:

```bash
dotnet test dotnet-practice.Tests/dotnet-practice.Tests.csproj
```

## Purpose

This repo exists to experiment, make mistakes safely, and learn how common .NET application patterns work in practice.
