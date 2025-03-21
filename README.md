# 🛒 Sales Management API – Developer Evaluation Project

![.NET 6.0](https://img.shields.io/badge/.NET-8.0-blueviolet)
![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework-Core%208.0-green)
![MediatR](https://img.shields.io/badge/MediatR-CQRS-lightgrey)
![RabbitMQ](https://img.shields.io/badge/RabbitMQ-Messaging-orange)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-Relational%20DB-blue)
![Docker](https://img.shields.io/badge/Docker-Containerization-blue)
![AutoMapper](https://img.shields.io/badge/AutoMapper-DTO%20Mapping-yellow)
![FluentValidation](https://img.shields.io/badge/FluentValidation-Validation-blue)
![Serilog](https://img.shields.io/badge/Serilog-Structured%20Logging-purple)

---

## 🌐 Overview

This project is a modular and containerized Sales Management RESTful API, developed with **.NET 8**, following best practices including **CQRS**, **DDD**, **Dependency Injection**, **Clean Architecture**, and **Asynchronous Messaging** using **RabbitMQ**.

---

## 🧠 Key Technologies

| Layer              | Technology            |
|--------------------|------------------------|
| Web Layer          | ASP.NET Core Web API   |
| Application Logic  | MediatR, CQRS          |
| Domain             | DDD, Business Rules    |
| Persistence        | EF Core + PostgreSQL   |
| Messaging Queue    | RabbitMQ               |
| Validation         | FluentValidation       |
| Object Mapping     | AutoMapper             |
| Logging            | Serilog                |
| Containers         | Docker, Docker Compose |
| Testing            | xUnit                  |

---

## 🗂️ Structure of the Solution

This project implements a robust Sales Management API using Domain Driven Design (DDD) principles and a clean, layered architecture. The key aspects of the solution include:

- **Layered Architecture:**  
  The solution is divided into several layers:
  - **Domain:** Contains entities (e.g., `User`, `Sale`, `SaleItem`), enums, and validation classes (e.g., `UserValidator`, `SaleValidator`, `SaleItemValidator`, `UpdateSaleValidator`).
  - **Application:** Implements use cases using CQRS with MediatR. CommandHandlers and QueryHandlers are implemented for CreateSale, UpdateSale, CancelSale, DeleteSale, CancelSaleItem, GetSale, and GetSaleItems.
  - **ORM:** Uses Entity Framework Core for persistence. The `DefaultContext` and mapping configurations (e.g., `SaleConfiguration`, `SaleItemConfiguration`) ensure the domain is correctly stored in PostgreSQL.
  - **IoC:** Manages dependency injection for all layers.
  - **WebAPI:** Exposes REST endpoints with JWT authentication and Swagger documentation. Controllers use a consistent response envelope.
  - **Messaging:** Domain events (SaleCreated, SaleModified, SaleCancelled, ItemCancelled) are published. Initially, events are logged via Serilog and then integrated with RabbitMQ.
  - **Testing:** Comprehensive unit tests are implemented using xUnit, Moq, EF Core InMemory, and FluentAssertions.

- **Security:**  
  JWT-based authentication protects endpoints, and Swagger is configured to allow token input (except on authentication routes).

- **Event Publishing and Message Broker:**  
  A simple integration with RabbitMQ is implemented. A custom `RabbitMQEventPublisher` publishes events to a dedicated exchange (`sales_exchange`) and queue (`sales_queue`). This allows for future consumer integration.

- **Containerization:**  
  Docker and Docker Compose orchestrate the application stack, including containers for WebAPI, PostgreSQL, MongoDB, Redis, and RabbitMQ. The compose files (docker-compose.yml and docker-compose.override.yml) ensure fixed ports and shared volumes.

- **Documentation:**  
  XML comments are used throughout the codebase to generate API documentation with Swagger, ensuring detailed endpoint documentation.

---

## 🗂️ Project Structure

```
sales-crud/
└── template/
    └── backend/
        ├── docker-compose.yml         # Orchestration of Web API, RabbitMQ, PostgreSQL
        ├── Dockerfile                 # API container build instructions
        ├── src/
        │   ├── Ambev.DeveloperEvaluation.WebApi/   # ASP.NET Core Web API entrypoint
        │   │   ├── Features/
        │   │   │   └── Sales/                      # API endpoints for Sale (Controller, Validators, DTOs)
        │   │   ├── Middleware/                     # Custom middlewares for error handling, logging etc.
        │   │   ├── Mappings/                       # AutoMapper profiles
        │   │   ├── Program.cs                      # Main entrypoint (minimal hosting)
        │   │   └── appsettings.json                # Config files
        │   │
        │   ├── Ambev.DeveloperEvaluation.Application/  # Application Layer (CQRS)
        │   │   ├── Sales/
        │   │   │   ├── CreateSale/
        │   │   │   ├── UpdateSale/
        │   │   │   ├── DeleteSale/
        │   │   │   └── GetSale/
        │   │   └── Users/                           # Business logic for user operations
        │   │
        │   ├── Ambev.DeveloperEvaluation.Domain/       # Domain entities and core business models
        │   │   └── Sale.cs                            # Aggregate root for sales
        │   │   └── SaleItem.cs                        # Sub-entity for line items
        │   │
        │   ├── Ambev.DeveloperEvaluation.ORM/          # EF Core setup and migrations
        │   │   └── DefaultContext.cs                  # DbContext implementation
        │   │   └── Migrations/                        # EF migrations folder
        │   │
        │   ├── Ambev.DeveloperEvaluation.IoC/          # Dependency injection configuration
        │   │   └── ServiceCollectionExtensions.cs     # Service registrations
        │   │
        │   └── Ambev.DeveloperEvaluation.Common/       # Shared abstractions
        │       └── Messaging/                         # Interfaces like IEventPublisher
        │       └── Responses/                         # ApiResponse, ApiResponseWithData
        │
        └── tests/                                      # xUnit test projects
            └── SalesTests/
                └── CreateSaleCommandHandlerTests.cs    # Unit tests for sales logic
```

---

## 🚀 Running the Application (via Docker)

### Prerequisites
- [.NET SDK 6.0+](https://dotnet.microsoft.com/)
- [Docker](https://www.docker.com/) and Docker Compose

### Start All Services

```bash
docker-compose up --build
```

This will boot:
- ✅ Web API (on port 5000)
- 🐘 PostgreSQL (default port 5432)
- 🧃 RabbitMQ (default port 5672)

---

## 🧱 Database Setup

### Create EF Core Migrations

```bash
dotnet ef migrations add InitialCreate --project src/Ambev.DeveloperEvaluation.ORM --startup-project src/Ambev.DeveloperEvaluation.WebApi
```

### Apply Migration

```bash
dotnet ef database update --project src/Ambev.DeveloperEvaluation.ORM --startup-project src/Ambev.DeveloperEvaluation.WebApi
```

Ensure the connection string in `appsettings.Development.json` is correct.

---

## ✨ Core Domain: Sales

This API provides full CRUD operations for Sales, with validation, domain logic and asynchronous event dispatching.

### 🧮 Business Rules
- Items quantity ≥ 10: 20% discount
- Quantity between 4-9: 10% discount
- Each item calculates total amount and discount

---

## 📬 Messaging with RabbitMQ

After a sale is created or deleted, a message is published using `IEventPublisher` abstraction.

```csharp
await _eventPublisher.PublishEventAsync("SaleCreated", new { SaleId = sale.Id }, cancellationToken);
```

This enables other systems (e.g. Inventory) to subscribe and react asynchronously.

---

## 📘 API Documentation

Base Route: `/api/sales`  
Authentication Required: ✅ Yes

---

### POST `/api/sales`

**Description:** Create a new sale.

```json
{
  "saleNumber": "string",
  "saleDate": "2024-03-21",
  "customer": "string",
  "branch": "string",
  "items": [
    {
      "product": "string",
      "quantity": 5,
      "unitPrice": 10.00
    }
  ]
}
```

**Responses:**
- ✅ `201 Created`
- ❌ `400 BadRequest`

---

### GET `/api/sales/{id}`

Get sale details by ID.

**Responses:**
- ✅ `200 OK`
- ❌ `404 NotFound`

---

### PUT `/api/sales/{id}`

Update a sale.

**Responses:**
- ✅ `200 OK`
- ❌ `400 BadRequest`, `404 NotFound`

---

### DELETE `/api/sales/{id}`

Delete a sale permanently.

---

### PATCH `/api/sales/{id}/cancel`

Soft-cancel a sale.

---

### GET `/api/sales/{saleId}/items`

Returns all items for a given sale.

---

### PATCH `/api/sales/{saleId}/items/{itemId}/cancel`

Cancel a specific item from a sale.

---

## 🧪 Running Tests

```bash
dotnet test
```

Unit and integration tests are found under `/backend/tests/`.

---

## 💡 Highlights

- ✅ Fully containerized (Docker)
- 🧠 CQRS + DDD Design
- ✉️ Async Events via RabbitMQ
- 🔒 Secure endpoints with [Authorize]
- 🛠️ Validation & Mapping for clean input/output
- 🧹 Separation of concerns: clean, testable code

---

## 👤 Rodney Victor

Project created for developer technical evaluation purposes. Designed with best practices for real-world scalable backend systems.

# Below we have the original evaluation proposal

# Developer Evaluation Project

`READ CAREFULLY`

## Instructions
**The test below will have up to 7 calendar days to be delivered from the date of receipt of this manual.**

- The code must be versioned in a public Github repository and a link must be sent for evaluation once completed
- Upload this template to your repository and start working from it
- Read the instructions carefully and make sure all requirements are being addressed
- The repository must provide instructions on how to configure, execute and test the project
- Documentation and overall organization will also be taken into consideration

## Use Case
**You are a developer on the DeveloperStore team. Now we need to implement the API prototypes.**

As we work with `DDD`, to reference entities from other domains, we use the `External Identities` pattern with denormalization of entity descriptions.

Therefore, you will write an API (complete CRUD) that handles sales records. The API needs to be able to inform:

* Sale number
* Date when the sale was made
* Customer
* Total sale amount
* Branch where the sale was made
* Products
* Quantities
* Unit prices
* Discounts
* Total amount for each item
* Cancelled/Not Cancelled

It's not mandatory, but it would be a differential to build code for publishing events of:
* SaleCreated
* SaleModified
* SaleCancelled
* ItemCancelled

If you write the code, **it's not required** to actually publish to any Message Broker. You can log a message in the application log or however you find most convenient.

### Business Rules

* Purchases above 4 identical items have a 10% discount
* Purchases between 10 and 20 identical items have a 20% discount
* It's not possible to sell above 20 identical items
* Purchases below 4 items cannot have a discount

These business rules define quantity-based discounting tiers and limitations:

1. Discount Tiers:
   - 4+ items: 10% discount
   - 10-20 items: 20% discount

2. Restrictions:
   - Maximum limit: 20 items per product
   - No discounts allowed for quantities below 4 items

## Overview
This section provides a high-level overview of the project and the various skills and competencies it aims to assess for developer candidates. 

See [Overview](/.doc/overview.md)

## Tech Stack
This section lists the key technologies used in the project, including the backend, testing, frontend, and database components. 

See [Tech Stack](/.doc/tech-stack.md)

## Frameworks
This section outlines the frameworks and libraries that are leveraged in the project to enhance development productivity and maintainability. 

See [Frameworks](/.doc/frameworks.md)

<!-- 
## API Structure
This section includes links to the detailed documentation for the different API resources:
- [API General](./docs/general-api.md)
- [Products API](/.doc/products-api.md)
- [Carts API](/.doc/carts-api.md)
- [Users API](/.doc/users-api.md)
- [Auth API](/.doc/auth-api.md)
-->

## Project Structure
This section describes the overall structure and organization of the project files and directories. 

See [Project Structure](/.doc/project-structure.md)

