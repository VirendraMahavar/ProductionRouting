# ProductionRouting – Ruleset Based Evaluation Engine

## Overview

This project implements a configurable Ruleset Evaluation Engine that determines the correct Production Plant for incoming Orders.

Supports:
- Dynamic rule configuration via JSON / Database
- Multi-layered Clean Architecture
- File Drop (ZIP/JSON)
- API Submission
- Background Processing
- NUnit Unit & Integration Tests

---

## Architecture

Layered Architecture:

- API Layer (Controllers)
- Application Layer (Services & Interfaces)
- Domain Layer (Rule Engine, Entities)
- Infrastructure Layer (EF Core, FileDropService, DB)

---

## Technologies Used

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- NUnit
- Clean Architecture Pattern

---

## Setup Instructions

1. Clone repository
2. Update connection string in appsettings.json
3. Run migrations:

