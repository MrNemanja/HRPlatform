# HR Platform (.NET 8)

This is a simple HR platform built with ASP.NET Core Web API (.NET 8) that allows managing candidates and their skills. The project covers basic CRUD operations, as well as candidate search and skill assignment.

---

## Features

- Add and delete candidates
- Add skills
- Assign and remove skills from candidates
- Search candidates by name and/or skills
- Data validation using DataAnnotations
- Unique constraints (email, phone number, skill name)
- Seed data for easier testing
- Unit tests for service and mapper layers

---

## Project Structure

The project is organized into several layers:

- Controllers – API endpoints
- Services – business logic
- Repositories – data access layer (EF Core)
- Models – entity classes representing database tables 
- DTOs – data transfer objects
- Mappers – mapping between entities and DTOs
- Data – DbContext and seeding
- Migrations – EF Core migrations used to create and update the database schema

---

## Database Seeder

The project includes a database seeder that fills the database with initial data so the application can be tested immediately.

Seeded data includes:
- several skills (C#, React, SQL, JavaScript, etc.)
- a few candidates with assigned skills

This removes the need for manual data entry when starting the application.

---

## Unit Tests

Unit tests are written for:
- CandidateService
- SkillService
- Mappers

Tests cover:
- successful scenarios
- business rules (e.g. uniqueness checks)
- exception scenarios
- search functionality

Moq is used for mocking repository dependencies.

---

## Most Challenging Part

The most challenging part for me was writing unit tests, since I hadn’t worked with them for a while. I had to refresh how mocking works and how to properly isolate the service layer so that tests don’t depend on the database.

Because of that, I decided to mock the repositories (using Moq) and test only the service layer instead of testing against the real database. This decision made the tests simpler, faster and focused only on business logic instead of infrastructure.

I also focused on covering both successful scenarios and cases where exceptions are expected, to ensure that business rules are properly validated.

This approach allows tests to be more reliable and easier to maintain.

This helped me better understand how important it is to separate business logic from data access.

---

## Technologies Used

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- MySQL
- xUnit
- Moq

---

## How to Run

1. Clone the repository
2. Set the connection string in `appsettings.json`
3. Run migrations
4. Run the application:

The seeder will automatically run on startup.

---

## Note

This project was built as a test assignment, with focus on:
- code organization
- clean architecture
- basic CRUD operations
- unit testing
