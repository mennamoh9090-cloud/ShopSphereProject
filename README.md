# ShopSphere 🛒

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture-0078D4)](#architecture--project-structure)
[![Database](https://img.shields.io/badge/Database-SQL%20Server-CC292B?logo=microsoft-sql-server&logoColor=white)](https://www.microsoft.com/sql-server)
[![UI](https://img.shields.io/badge/UI-ASP.NET%20Core%20MVC%20%2B%20Bootstrap%205-7952B3?logo=bootstrap&logoColor=white)](https://getbootstrap.com/)
[![API](https://img.shields.io/badge/API-RESTful%20%2B%20Swagger-85EA2D?logo=swagger&logoColor=black)](#api-overview)

**ShopSphere** is an enterprise-grade e-commerce web application built on **ASP.NET Core 8** adhering strictly to **Clean Architecture** principles. The solution features a decoupled RESTful Web API backend, an interactive server-rendered MVC storefront, a shopping cart with guest-to-user session migration, role-based administrative dashboards, and database persistence powered by Entity Framework Core 8 and SQL Server.

---

## 📑 Table of Contents

- [Overview & Business Purpose](#overview--business-purpose)
- [Key Features](#key-features)
- [Technology Stack](#technology-stack)
- [Architecture & Project Structure](#architecture--project-structure)
- [Main Modules & Responsibilities](#main-modules--responsibilities)
- [Prerequisites](#prerequisites)
- [Installation & Quick Start](#installation--quick-start)
- [Database Setup & Migrations](#database-setup--migrations)
- [Configuration & Environment Variables](#configuration--environment-variables)
- [How to Run the Application](#how-to-run-the-application)
- [API Overview](#api-overview)
- [Authentication & Authorization](#authentication--authorization)
- [External Integrations](#external-integrations)
- [Testing & Quality Verification](#testing--quality-verification)
- [Common Troubleshooting](#common-troubleshooting)
- [Security Considerations](#security-considerations)
- [Documentation Index](#documentation-index)
- [Contributing](#contributing)

---

## 🎯 Overview & Business Purpose

The primary goal of ShopSphere is to deliver a smooth, high-performance online retail experience. Shoppers can seamlessly browse product catalogs, filter items by category, execute real-time searches, manage shopping carts as guests or authenticated users, and submit orders with flexible payment options (Cash on Delivery or Card). 

Store administrators have access to a dedicated portal providing real-time sales KPIs, catalog management (CRUD), category categorization, and order fulfillment status updates.

---

## ✨ Key Features

* **Clean Architecture**: Decoupled layers separating Domain entities, Application business logic, Infrastructure persistence, and Presentation layers.
* **RESTful Web API**: Modular API with Swagger/OpenAPI interactive documentation, DTO mapping, and FluentValidation rules.
* **Modern Storefront (MVC)**: Responsive customer UI built with ASP.NET Core Razor Pages and Bootstrap 5.
* **Cart with Session Migration**: Allows anonymous users to shop freely; items are automatically migrated to the user's permanent account upon registration or login.
* **Checkout & Taxation Engine**: Dynamic calculation of subtotals, tax rates (4%), and flexible payment modes (Cash On Delivery & simulated card payments).
* **Role-Based Admin Dashboard**: Complete administrative toolset for monitoring sales, managing inventory stock, adjusting product details, and updating order fulfillment states.
* **Structured Logging**: Built-in Serilog logging pipeline streaming events to both console and rolling daily log files.
* **Pre-seeded Sample Data**: Automatic seeding of 5 categories and 12 ready-to-use products with imagery and stock levels.

---

## 🛠️ Technology Stack

| Layer / Aspect | Technology | Version / Details |
| :--- | :--- | :--- |
| **Framework** | .NET | `8.0` (C# 12) |
| **Architecture** | Clean Architecture / Onion | Domain, Application, Infrastructure, API, MVC |
| **Web API** | ASP.NET Core Web API | Controllers, Swagger / Swashbuckle 8.0 |
| **Web UI** | ASP.NET Core MVC | Razor Views, Bootstrap 5, jQuery |
| **Database & ORM** | Microsoft SQL Server / LocalDB | Entity Framework Core `8.0.0` |
| **Authentication** | ASP.NET Core Identity & Cookie Auth | `CookieAuthenticationDefaults`, Identity Roles |
| **Object Mapping** | AutoMapper | `16.1.1` |
| **Validation** | FluentValidation | `11.9.0` |
| **Logging** | Serilog | `Serilog.AspNetCore 8.0.1`, File & Console sinks |
| **Payments** | Stripe.net | `50.5.0-beta.1` (Configured for integration) |

---

## 🏛️ Architecture & Project Structure

ShopSphere is organized into 5 projects structured under the Clean Architecture pattern:

```
ShopSphere/
├── ShopSphere.Domain/               # Core Enterprise Domain Entities & Repository Contracts
│   ├── Entities/                    # Product, Category, Order, OrderItem, Cart, Customer, User
│   └── Repositories/                # IProductRepository, ICategoryRepository, IOrderRepository, IUnitOfWork
├── ShopSphere.Application/          # Application Business Logic & Orchestration
│   ├── DTOs/                        # ProductDto, CategoryDto, OrderDto, CartDto, CheckoutDto
│   ├── Mapping/                     # AutoMapper Profile (MappingProfile.cs)
│   ├── Services/                    # ProductService, CategoryService, OrderService
│   └── Validators/                  # FluentValidation validators (Product, Category, Order)
├── ShopSphere.Infrastructure/       # Data Access, Persistence & Database Context
│   ├── DbContext/                   # ApplicationDbContext, DataSeeder
│   ├── Migrations/                  # EF Core Migrations (InitialMigration, SeedUpdates)
│   └── Repository/                  # GenericRepository, ProductRepository, UnitOfWork
├── ShopSphere.API/                  # RESTful Web API Layer
│   ├── Controllers/                 # ProductsController, CategoriesController, OrdersController, DashboardController
│   ├── Middleware/                  # GlobalExceptionMiddleware
│   └── Program.cs                   # API service registrations, Serilog, Swagger & Identity seeding
├── ShopSphere.MVC/                  # Client Presentation Layer (Razor & Bootstrap)
│   ├── Controllers/                 # HomeController, ProductController, CartController, CheckoutController, AdminController
│   ├── Models/                      # ViewModels (CartViewModel, OrderViewModel, AdminDashboardViewModel)
│   ├── Services/                    # ApiService (Typed HttpClient communicating with API)
│   ├── Views/                       # Razor Views (Home, Product, Cart, Checkout, Admin, Account)
│   └── wwwroot/                     # Static assets (Bootstrap, CSS, JS, Images)
├── docs/                            # In-depth technical documentation guides
├── ShopSphere.sln                   # Visual Studio Solution File
└── ShopSphere.slnLaunch.user        # Multi-project startup profile
```

For full architectural diagrams and design pattern explanations, see the [System Architecture Guide](docs/architecture.md).

---

## 📦 Main Modules & Responsibilities

| Project | Responsibility | Dependencies |
| :--- | :--- | :--- |
| **`ShopSphere.Domain`** | Enterprise entities, aggregates, and repository interfaces. | Microsoft.Extensions.Identity.Core |
| **`ShopSphere.Application`** | DTOs, AutoMapper profiles, FluentValidation, domain services. | `ShopSphere.Domain`, AutoMapper, FluentValidation |
| **`ShopSphere.Infrastructure`** | EF Core DbContext, SQL Server mappings, Repository implementations, migrations. | `ShopSphere.Domain`, `ShopSphere.Application`, EF Core |
| **`ShopSphere.API`** | Exposes HTTP REST endpoints, swagger documentation, global exception handling. | `ShopSphere.Application`, `ShopSphere.Infrastructure` |
| **`ShopSphere.MVC`** | Server-rendered frontend, cookie auth, session cart, admin UI, consumes API. | `ShopSphere.Application`, `ShopSphere.Infrastructure` |

---

## 📋 Prerequisites

Before setting up the project, make sure you have:
* **[.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)** or higher installed.
* **Microsoft SQL Server LocalDB** (shipped with Visual Studio) or **SQL Server Express**.
* **Visual Studio 2022** (v17.8+) with ASP.NET and web development workload, or **VS Code**.
* **Entity Framework Core CLI Tools**:
  ```bash
  dotnet tool install --global dotnet-ef
  ```

---

## 🚀 Installation & Quick Start

### 1. Clone the Repository
```bash
git clone https://github.com/mennamoh9090-cloud/ShopSphereProject.git
cd ShopSphereProject
```

### 2. Restore Dependencies
```bash
dotnet restore ShopSphere.sln
```

### 3. Build the Solution
```bash
dotnet build ShopSphere.sln
```

---

## 🗄️ Database Setup & Migrations

The project uses EF Core with SQL Server. The default connection string targets SQL Server LocalDB (`(localdb)\MSSQLLocalDB`).

### Apply Migrations
Apply the initial schema and pre-configured seed data by running:
```bash
dotnet ef database update --project ShopSphere.Infrastructure --startup-project ShopSphere.API
```

This creates the database `ShopSphereDb` and seeds:
- 5 Product Categories (Electronics, Apparel, Books, Appliances, Gaming).
- 12 Ready-to-display Products with prices, stock, and photos.
- Identity Roles: `Admin` and `User`.

For detailed entity diagrams and schema details, see the [Database & Migrations Guide](docs/database-and-migrations.md).

---

## ⚙️ Configuration & Environment Variables

### `ShopSphere.API/appsettings.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=ShopSphereDb;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False"
  },
  "Jwt": {
    "Key": "YourSecretKeyWithAtLeast32CharactersHere!",
    "Issuer": "ShopSphereAPI",
    "Audience": "ShopSphereClient",
    "DurationInDays": 30
  },
  "Stripe": {
    "SecretKey": "sk_test_PLACEHOLDER",
    "PublishableKey": "pk_test_PLACEHOLDER",
    "WebhookSecret": "whsec_PLACEHOLDER"
  }
}
```

> [!NOTE]
> In production environments, sensitive values such as connection strings and JWT keys should be injected using Environment Variables or Secret Managers.

---

## 💻 How to Run the Application

The system requires running both the **API** and the **MVC frontend** concurrently.

### Option 1: Visual Studio 2022 (Multi-Project Startup)
1. Open `ShopSphere.sln`.
2. The solution includes a multi-project launch profile (`ShopSphere.slnLaunch.user`).
3. Press **F5** or click **Start**.
4. Both projects will launch:
   - **API**: `http://localhost:5062` (Swagger UI opens at `http://localhost:5062/swagger`)
   - **MVC**: `http://localhost:5107` (Opens the storefront in your default browser)

### Option 2: .NET CLI (Two Terminals)

**Terminal 1 — API Backend:**
```bash
dotnet run --project ShopSphere.API --launch-profile http
```

**Terminal 2 — MVC Storefront:**
```bash
dotnet run --project ShopSphere.MVC --launch-profile http
```

Once running:
- **Storefront**: [`http://localhost:5107`](http://localhost:5107)
- **API Swagger**: [`http://localhost:5062/swagger`](http://localhost:5062/swagger)

---

## 🌐 API Overview

The API provides endpoints across four primary resource controllers:

| Area | Route | Methods | Description |
| :--- | :--- | :--- | :--- |
| **Products** | `/api/products` | `GET`, `POST` | List/search products or create a new product. |
| **Products** | `/api/products/{id}` | `GET`, `PUT`, `DELETE` | Retrieve, edit, or delete a specific product. |
| **Categories** | `/api/categories` | `GET`, `POST` | List categories or create a new category. |
| **Categories** | `/api/categories/{id}` | `GET`, `PUT`, `DELETE` | Retrieve, edit, or delete a specific category. |
| **Orders** | `/api/orders` | `GET` | Retrieve list of customer orders. |
| **Orders** | `/api/orders/checkout` | `POST` | Submit checkout order and persist line items. |
| **Orders** | `/api/orders/{id}/status` | `PUT` | Update order processing status. |
| **Dashboard** | `/api/dashboard/admin` | `GET` | Aggregate stats (Total sales, counts, recent products). |
| **Dashboard** | `/api/dashboard/users` | `GET` | List registered user directory. |

For complete request/response schemas and examples, see the [REST API Reference](docs/api-reference.md).

---

## 🔐 Authentication & Authorization

* **Storefront Authentication**: Built on ASP.NET Core **Cookie Authentication** (`CookieAuthenticationDefaults.AuthenticationScheme`) with cookie name `ShopSphere.Auth`.
* **Role-Based Redirection**:
  * Any user email containing `"admin"` (e.g., `admin@shopsphere.com`) is granted the `Admin` role and redirected to the `/Admin/Dashboard`.
  * Standard emails receive the `User` role and are redirected to `/User/Dashboard`.
* **Guest-to-User Cart Migration**: When a guest adds items to their cart and subsequently logs in or registers, the anonymous cart session is automatically migrated to their authenticated user session without losing items.

For sequence diagrams of the authentication and cart migration flows, see the [Workflows & Features Guide](docs/workflows-and-features.md).

---

## 🔌 External Integrations

* **Payment Processing**: The system provides payment method selection at checkout:
  * **Cash on Delivery (COD)**: Records `OrderStatus = "Pending"`, `PaymentStatus = "Pending"`.
  * **Card Payment (Mocked)**: Emulates online card confirmation, recording `PaymentStatus = "Paid"` with a generated mock session identifier (`mock_{guid}`).
  * The solution includes `Stripe.net` dependencies and configuration sections in `appsettings.json` to facilitate direct Stripe webhook and payment intent integration.

---

## 🧪 Testing & Quality Verification

* **Solution Compilation**: Verified using `dotnet build ShopSphere.sln` (.NET 8.0 SDK, 0 Errors).
* **Interactive Testing**:
  * Execute API contracts and test request/response payloads directly via Swagger UI at `http://localhost:5062/swagger`.
  * Test end-to-end shopping journeys (Catalog browsing, Cart additions, Checkout submission, Admin updates) through the MVC frontend at `http://localhost:5107`.
* **Unit Testing Roadmap**: The solution architecture is designed for unit testing with repository mocks (`Moq` / `NSubstitute`) and in-memory database testing (`xUnit`). Automated test suites can be added to the solution targeting `ShopSphere.Application` and `ShopSphere.API`.

---

## 🩺 Common Troubleshooting

| Issue | Likely Cause | Solution |
| :--- | :--- | :--- |
| **LocalDB Connection Error** | `MSSQLLocalDB` service is stopped | Run `sqllocaldb start MSSQLLocalDB` in terminal. |
| **MVC Catalog Empty** | `ShopSphere.API` is not running on port 5062 | Ensure the API project is running at `http://localhost:5062`. |
| **Invalid Object Name 'Products'** | Database migrations not applied | Run `dotnet ef database update --project ShopSphere.Infrastructure --startup-project ShopSphere.API`. |
| **Port Conflict (5062 / 5107)** | Port already occupied by another process | Stop the process or change ports in `Properties/launchSettings.json`. |

For step-by-step diagnostic procedures, check the [Troubleshooting Guide](docs/troubleshooting.md).

---

## 🔒 Security Considerations

* **Secrets Management**: Do not commit real connection strings, private JWT keys, or Stripe secret keys to version control. Use `.gitignore` and environment variables.
* **Database Queries**: All queries are executed using EF Core parameterized queries, preventing SQL injection vulnerabilities.
* **Input Validation**: Critical inputs are validated through `FluentValidation` validators before reaching the persistence layer.
* **Cookies**: Session cookies are configured with `HttpOnly = true` to protect against cross-site scripting (XSS) attacks.

---

## 📚 Documentation Index

Detailed documentation guides are organized in the [`docs/`](docs/) directory:

* 📐 **[System Architecture](docs/architecture.md)** — Architectural design, layers, dependencies, and design patterns.
* 📡 **[REST API Reference](docs/api-reference.md)** — Detailed API endpoints, parameters, request/response schemas.
* 🗄️ **[Database & Migrations](docs/database-and-migrations.md)** — ERD diagram, entity relations, migration commands, and seeding.
* 🚀 **[Setup & Deployment](docs/setup-and-deployment.md)** — Step-by-step local setup, multi-project execution, and deployment tips.
* 🔄 **[Workflows & Features](docs/workflows-and-features.md)** — Cart session migration, checkout sequence, and admin workflows.
* 🛠️ **[Troubleshooting](docs/troubleshooting.md)** — Diagnostic steps for LocalDB, ports, and inter-service connectivity.

---

## 👥 Contributing

Contributions, issues, and feature requests are welcome!

1. Fork the Project.
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`).
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`).
4. Push to the Branch (`git push origin feature/AmazingFeature`).
5. Open a Pull Request.

---

## 📄 License

This project is licensed under the MIT License - see existing project headers for details.