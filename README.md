# Order Management System

A comprehensive RESTful API for managing orders, customers, products, invoicing, and user authentication built with ASP.NET Core 6.0.

## Project Overview

The Order Management System is a robust backend API that handles the complete lifecycle of e-commerce order processing. It provides secure user authentication via JWT tokens, role-based authorization (Admin/Customer), inventory management, automatic discount calculations, invoice generation, and email notifications for order status updates.

**Key Goals:**
- Streamline order processing workflows from creation to fulfillment
- Provide secure authentication and role-based access control
- Automate invoice generation and customer notifications
- Maintain real-time inventory tracking with stock validation
- Offer a clean RESTful API with Swagger documentation

**Use Cases:**
- Small to medium e-commerce platforms
- Internal order management for retail businesses
- Learning/demonstration of clean architecture patterns in .NET
- Foundation for custom order processing systems

## Tech Stack

**Languages & Frameworks:**
- C# / .NET 6.0
- ASP.NET Core Web API 6.0

**Libraries & Packages:**
- `Microsoft.EntityFrameworkCore` (6.0.0) - ORM for data access
- `Microsoft.EntityFrameworkCore.InMemory` (6.0.0) - In-memory database for development/testing
- `Microsoft.AspNetCore.Authentication.JwtBearer` (6.0.0) - JWT authentication
- `BCrypt.Net-Next` (4.0.3) - Password hashing
- `Swashbuckle.AspNetCore` (6.2.3) - Swagger/OpenAPI documentation
- `xUnit` (2.4.2) - Unit testing framework

**Tooling:**
- Visual Studio 2022 (inferred from .vs folder structure)
- .NET CLI (for build/run operations)

## Architecture

**High-Level Design:**
The application follows a layered architecture pattern with clear separation of concerns:

```
Presentation Layer (Controllers)
         ↓
Service Layer (Business Logic)
         ↓
Repository Layer (Data Access)
         ↓
Data Layer (EF Core + In-Memory DB)
```

**Major Components:**
1. **Controllers** - HTTP endpoints for Customer, Order, Product, Invoice, and User operations
2. **Services** - Business logic (OrderService handles order creation, discount calculation, invoice generation)
3. **Repositories** - Data access abstraction (CRUD operations for each entity)
4. **Models** - Domain entities (Customer, Order, Product, Invoice, User)
5. **DTOs** - Data transfer objects for API requests/responses
6. **Authentication** - JWT-based authentication with role-based authorization

**Data Flow:**
1. Client sends HTTP request with JWT token
2. Authentication middleware validates token
3. Controller receives request and delegates to Service layer
4. Service layer applies business rules and calls Repository
5. Repository interacts with EF Core DbContext
6. Response flows back through layers to client

## Features

**Core Features:**
- ✅ User registration and login with JWT authentication
- ✅ Role-based authorization (Admin & Customer roles)
- ✅ Product catalog management (CRUD operations)
- ✅ Customer management with order history retrieval
- ✅ Order creation with automatic stock validation
- ✅ Dynamic discount calculation (5% for orders $100+, 10% for $200+)
- ✅ Automatic invoice generation upon order creation
- ✅ Order status tracking (Pending, Processing, Shipped, Delivered, Cancelled)
- ✅ Email notifications for order status updates
- ✅ Swagger UI for API documentation and testing

**Additional Features:**
- In-memory database for rapid development/testing
- Password encryption using BCrypt
- EF Core navigation properties for relational data
- Repository pattern for testable data access
- Unit tests with xUnit framework

## Setup & How to Run

**Prerequisites:**
- .NET 6.0 SDK ([Download here](https://dotnet.microsoft.com/download/dotnet/6.0))
- Visual Studio 2022 (optional, or any IDE with .NET support)
- Git (for cloning the repository)

**Installation Steps:**

1. **Clone the repository:**
   ```bash
   git clone <repository-url>
   cd OrderManagementSystem
   ```

2. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

3. **Configure application settings:**
   - Review `appsettings.json` and update JWT settings (see Configuration section)

4. **Build the project:**
   ```bash
   dotnet build
   ```

5. **Run the application:**
   ```bash
   cd OrderManagementSystem
   dotnet run
   ```

6. **Access the application:**
   - Swagger UI: `https://localhost:5001` (or `http://localhost:5000`)
   - API Base URL: `https://localhost:5001/api`

**Common Commands:**
```bash
# Build solution
dotnet build

# Run application
dotnet run --project OrderManagementSystem

# Run tests
dotnet test

# Clean build artifacts
dotnet clean

# Watch mode (auto-reload on changes)
dotnet watch run --project OrderManagementSystem
```

## Testing

**Running Tests:**
```bash
# Run all tests
dotnet test

# Run with detailed output
dotnet test --logger "console;verbosity=detailed"

# Run specific test class
dotnet test --filter "FullyQualifiedName~OrderServiceTests"
```

**Test Coverage:**
- Unit tests for OrderService (CreateOrderAsync, stock validation)
- Tests use in-memory database for isolation
- Current coverage: Core order creation and validation logic

**Notes:**
- Tests are located in the `Tests` project
- Uses xUnit as the testing framework
- Additional test coverage recommended for Controllers and Repositories

## Folder Structure

```
OrderManagementSystem/
├── OrderManagementSystem/          # Main API project
│   ├── Controllers/                # API endpoints
│   │   ├── CustomerController.cs
│   │   ├── OrderController.cs
│   │   ├── ProductController.cs
│   │   ├── InvoiceController.cs
│   │   └── UserController.cs
│   ├── Services/                   # Business logic layer
│   │   ├── IOrderService.cs
│   │   ├── OrderService.cs
│   │   ├── IEmailService.cs
│   │   └── EmailService.cs
│   ├── Repositories/               # Data access layer
│   │   ├── Implementations/
│   │   │   ├── CustomerRepository.cs
│   │   │   ├── OrderRepository.cs
│   │   │   ├── ProductRepository.cs
│   │   │   └── UserRepository.cs
│   │   └── I*Repository.cs         # Repository interfaces
│   ├── Models/                     # Domain entities
│   │   ├── Customer.cs
│   │   ├── Order.cs
│   │   ├── OrderItem.cs
│   │   ├── Product.cs
│   │   ├── Invoice.cs
│   │   └── User.cs
│   ├── DTOs/                       # Data transfer objects
│   │   ├── OrderCreateDto.cs
│   │   ├── OrderStatusUpdateDto.cs
│   │   └── UserLoginDto.cs
│   ├── Data/                       # Database context
│   │   └── OrderManagementDbContext.cs
│   ├── Properties/
│   │   └── launchSettings.json     # Development server configuration
│   ├── Program.cs                  # Application entry point
│   ├── Startup.cs                  # Service configuration
│   └── appsettings.json            # Application configuration
└── Tests/                          # Unit tests project
    ├── OrderServiceTests.cs
    └── OrderManagementSystem.Tests.csproj
```

## Configuration & Secrets

**appsettings.json Configuration:**

```json
{
  "Jwt": {
    "Key": "YOUR_SECRET_KEY_HERE_MIN_32_CHARS",
    "Issuer": "OrderManagementSystem",
    "Audience": "OrderManagementSystem",
    "ExpiryInDays": "7"
  },
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_DATABASE_CONNECTION_STRING"
  }
}
```

**Environment Variables (for production):**
- `JWT_KEY` - Secret key for JWT token signing (min 32 characters)
- `JWT_ISSUER` - Token issuer identifier
- `JWT_AUDIENCE` - Token audience identifier
- `ASPNETCORE_ENVIRONMENT` - Set to "Production" for production deployment

**Security Notes:**
- ⚠️ Change the JWT secret key before deploying to production
- Never commit real secrets to version control
- Use environment variables or Azure Key Vault for production secrets
- The current in-memory database is for development only - configure a real database (SQL Server, PostgreSQL) for production

## Screenshots / Demo

**Suggested Screenshots:**
1. `swagger-ui.png` - Swagger documentation interface showing all API endpoints
2. `order-creation.png` - POST request to create order with response showing generated invoice
3. `jwt-authentication.png` - Login endpoint returning JWT token
4. `order-status-update.png` - Admin updating order status
5. `product-management.png` - Product CRUD operations in Swagger

**Demo Workflow:**
1. Register a new user via `/api/user/register`
2. Login and obtain JWT token via `/api/user/login`
3. Add "Bearer {token}" to Swagger authorization
4. Create products via `/api/product` (Admin only)
5. Create an order via `/api/order` - observe automatic discount and invoice generation
6. View order details and invoices

## Future Improvements

**High Priority:**
- [ ] Integrate real email service (SendGrid, SMTP) instead of console logging
- [ ] Add real database support (SQL Server, PostgreSQL) with migrations
- [ ] Implement pagination for list endpoints
- [ ] Add comprehensive error handling and logging (Serilog)
- [ ] Implement request validation using FluentValidation
- [ ] Add API rate limiting and throttling

**Medium Priority:**
- [ ] Implement payment gateway integration (Stripe, PayPal)
- [ ] Add order cancellation and refund logic
- [ ] Create admin dashboard for analytics
- [ ] Add product search and filtering capabilities
- [ ] Implement audit logging for critical operations
- [ ] Add Docker support with docker-compose

**Nice to Have:**
- [ ] GraphQL endpoint option
- [ ] Real-time order status updates via SignalR
- [ ] Multi-tenancy support
- [ ] Advanced reporting and export features (PDF invoices, CSV exports)
- [ ] Integration tests with TestContainers
- [ ] CI/CD pipeline configuration (GitHub Actions, Azure DevOps)

## Contribution Guidelines

We welcome contributions! Please follow these guidelines:

**How to Contribute:**
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Make your changes following the existing code style
4. Write or update tests for your changes
5. Commit with clear messages (`git commit -m 'Add amazing feature'`)
6. Push to your branch (`git push origin feature/amazing-feature`)
7. Open a Pull Request

**PR Standards:**
- Provide a clear description of the changes
- Reference any related issues
- Ensure all tests pass
- Follow C# naming conventions and SOLID principles
- Update documentation if adding new features

**Code Style:**
- Use meaningful variable/method names
- Follow async/await patterns consistently
- Keep methods small and focused (Single Responsibility)
- Add XML documentation comments for public APIs

## License

**MIT License** - This project is licensed under the MIT License, making it free to use, modify, and distribute. This permissive license is ideal for open-source learning projects and allows commercial use while limiting liability.


## Social Links / Author Contact

**Project Maintainer:** [Ahmed Khaled]

- GitHub: [@yourusername](https://github.com/ahmed-khalid2004)
- LinkedIn: [Your Profile](https://linkedin.com/in/ahmed-khalid-5b6349259)
- Email: engahmedkhalid3s@gmail.com
- Project Issues: [GitHub Issues](https://github.com/ahmed-khalid2004/OrderManagementSystem/issues)

---

**What I looked at:** Analyzed the complete source code including Controllers, Services, Repositories, Models, DTOs, Startup.cs, Program.cs, appsettings.json, OrderManagementSystem.csproj, test files, and the provided directory tree structure to understand the architecture, dependencies, and functionality of this ASP.NET Core 6.0 order management system.
