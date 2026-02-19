# ProductionRouting – Ruleset Based Evaluation Engine
Determine Production Plant Using Ruleset Evaluation : 
Business Context
Your organization manages printing and production orders that come from multiple external
and internal clients.
Each Order must be routed to the correct Production Plant based on a set of business
rules that vary depending on the publisher, order method, country, and print quantity.
The Production team currently performs this decision manually.
The goal of this project is to automate the routing decision through a configurable
Ruleset-based Evaluation System.

Objective
Design and implement a Ruleset Evaluation Engine that can determine the appropriate
Production Plant for a given Order JSON input.
Your solution must support:
 Dynamic rule configuration (via database or JSON file)
 Evaluation logic that matches incoming orders to the correct plant
 Clean, scalable architecture suitable for enterprise use
 Auditable logging and test coverage
This assignment will evaluate your architecture design, code quality, extensibility, and
ability to communicate your solution clearly.

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
3. Run migrations: (Database can be created either via EF migrations or by running the SQL script located in /db folder.)
   
4. Run API:

---

## API Endpoint

POST /api/orders

Example JSON available in /docs folder.

---

## File Drop

Drop JSON or ZIP files in configured folder:

C:\OrderDrop : update the folder into the appsetting.json -- "FileDrop"
Add Sample directly .json files else .zip folders which contains .json files

System processes automatically.

---

## Testing

Run:

dotnet test : 
Includes:

- RuleEngine tests
- Condition tests
- OrderFactExtractor tests
- Integration tests

Add SQL Script
Add Sample JSON Files

---

## Author

Virendra Mahavar
