# Exim Stock Transactions Solution

## Overview

This project provides CRUD endpoints for managing inventory items and recording stock transactions. 

It also has a front-end build using Blazor WASM which interacts with the aforementioned backend.

## Architecture
This project follows Clean Architecture principles, separating concerns across layers: Api, Domain, Application, and Infrastructure.  
Tests are located in a separate project.

### Layering

#### API Layer

- Exposes REST endpoints via controllers.  
- Accepts HTTP requests, validates input, calls Application services, and returns HTTP responses.  
- Centralized exception middleware ensures consistent error handling and JSON error responses.

#### Application Layer

- Contains service interfaces (_IItemRepository_, _IStockTransactionService_) and orchestrates use cases.  
- Handles business logic and ensures validation before persisting data.

#### Domain Layer

- Contains core business entities (_Item_, _StockTransaction_) as C# classes.

#### Infrastructure Layer

- Implements repository interfaces using EF Core and SQLite.  
- Handles external dependencies like databases and logging.

### Key Design Decisions

#### Repository Interfaces in Application Layer

- Keeps Domain and Application decoupled from Infrastructure.  
- Allows swapping database implementations without changing business logic.

#### Exception Middleware

- All unhandled exceptions bubble up to middleware.  
- Returns structured JSON with _success_, _error_, and _traceId_.  
- Prevents stack traces from leaking to clients.

#### Database Design

- SQLite was chosen for simplicity.  
- EF Core mappings use the decimal type for monetary fields to preserve precision.  
- Trade-off: SQLite may not scale to heavy multi-user environments.

#### Endpoint Logic in Application Services

- Endpoint logic currently resides in Application layer services.  
- Trade-off: Each endpoint should delegate its logic to Handlers for better separation of concerns and testability. Handlers would add some boilerplate but improve maintainability.

### Testing

- Manual testing via UI. API endpoints tested with Postman.
- Exception handling tested by submitting duplicate SKUs or invalid IDs.  
- Automated tests exist for some services using xUnit and Moq.  
- Limitations: No automated API unit tests due to time constraints.