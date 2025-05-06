# ETL Pipeline Project

This project is designed for **ETL (Extract, Transform, Load)** operations.  
It is architected with **CQRS**, **Repository Pattern**, **background jobs**, **caching**, and **observability**, and is prepared for a future migration to microservices.

---

## Main Features

- **Extract** data from multiple sources (SQL, NoSQL, Files, APIs).
- **Transform** data based on flexible, configurable rules.
- **Load** data into multiple destination systems.
- **Schedule** and **automate** ETL jobs with retry & recovery.
- **Monitor** ETL pipelines with logging, metrics, and health checks.
- **Caching** to optimize ETL performance (e.g., Redis).
- **Modern architecture**: clean separation of concerns, scalable and maintainable code.

---

## Technologies Used

| Category             | Technology                        |
| -------------------- | --------------------------------- |
| Backend Framework    | ASP.NET Core Web API (.NET 8 LTS) |
| ORM                  | Entity Framework Core 8          |
| Background Jobs      | Hangfire                          |
| Caching              | Redis (StackExchange.Redis)       |
| Logging & Tracing    | Serilog + OpenTelemetry           |
| API Documentation    | Swagger (Swashbuckle.AspNetCore)  |
| Database             | PostgreSQL / SQL Server, MongoDB  |
| Dependency Injection | .NET Built-in DI                  |
| Testing              | xUnit + FluentAssertions          |

---

## Project Structure

```plaintext
src/
├── Api/
│   ├── Controllers/        # API entry points
│   ├── Extensions/          # Swagger, Middleware setups
│   └── Middlewares/         # Custom middleware (e.g., error handling)
├── Application/
│   ├── Commands/            # Command Handlers (CQRS)
│   ├── Queries/             # Query Handlers (CQRS)
│   ├── Dtos/                # Data Transfer Objects
│   └── Services/            # Pipeline orchestration logic
├── Domain/
│   ├── Entities/            # Core business entities
│   ├── Events/              # Domain events
│   └── ValueObjects/        # Value objects
├── Infrastructure/
│   ├── Extractors/          # Data source connectors (SQL, API, etc.)
│   ├── Transformers/        # Data transformation logic
│   ├── Loaders/             # Data loaders to destination systems
│   ├── Repositories/        # Data persistence logic
│   ├── BackgroundJobs/      # Hangfire recurring jobs
│   └── ExternalClients/     # API clients, file readers, etc.
├── Shared/
│   ├── Caching/             # Redis caching helpers
│   ├── Logging/             # Logging and telemetry
│   ├── Scheduling/          # Scheduler interfaces
│   └── Exceptions/          # Global exception handling
```

---

## Design Principles

- **Bounded Contexts** even inside Monolith (modular structure).
- **CQRS Pattern**: separate command and query responsibilities.
- **Repository Pattern**: abstract data access logic.
- **Event-driven** internal architecture (ready for message brokers later).
- **Observability**: centralized logging, metrics, and distributed tracing.
- **OpenAPI Contract-first**: clear API definitions for future service split.

---

## How To Run

1. Clone the repository:
   ```bash
   git clone https://github.com/ThanggDuongg/ETL-Pipeline-Tool-NET.git
   ```

TBD

---

## Result

TBD

---

## Future Migration to Microservices

- **Split by bounded context** (Extractor Service, Transformer Service, Loader Service).
- **Introduce Message Bus** (RabbitMQ, Kafka).
- **Keep Redis, databases, and background jobs distributed**.
- **Reuse OpenTelemetry tracing** across services.
- **Each service exposes its own OpenAPI contract**.

---
