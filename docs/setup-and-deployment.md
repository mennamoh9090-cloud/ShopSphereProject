# Setup & Deployment Guide

This guide provides end-to-end instructions for configuring, running, and deploying **ShopSphere** on local development machines and production environments.

---

## 1. Prerequisites

Ensure the following tools are installed before setting up the application:

| Tool / Runtime | Minimum Version | Installation Link |
| :--- | :--- | :--- |
| **.NET SDK** | `8.0.x` | [Download .NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0) |
| **SQL Server** | LocalDB / SQL Server Express | [Download SQL Server](https://www.microsoft.com/sql-server) |
| **IDE** | Visual Studio 2022 (17.8+) or VS Code | [Visual Studio 2022](https://visualstudio.microsoft.com/) / [VS Code](https://code.visualstudio.com/) |
| **EF Core Tools** | `8.0.x` | `dotnet tool install --global dotnet-ef` |

---

## 2. Local Development Setup

### Step 1: Clone the Repository
```bash
git clone https://github.com/mennamoh9090-cloud/ShopSphereProject.git
cd ShopSphereProject
```

### Step 2: Restore Dependencies
```bash
dotnet restore ShopSphere.sln
```

### Step 3: Configure Database Connection
Verify the connection string in `ShopSphere.API/appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=ShopSphereDb;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False"
}
```

> [!TIP]
> If you are using full SQL Server or a remote instance, update `Server=(localdb)\\MSSQLLocalDB` to your SQL Server instance name (e.g. `Server=localhost\\SQLEXPRESS` or `Server=127.0.0.1;User Id=sa;Password=...`).

### Step 4: Apply Database Migrations
Initialize the database and apply all seeded data:
```bash
dotnet ef database update --project ShopSphere.Infrastructure --startup-project ShopSphere.API
```

---

## 3. Running the Application

ShopSphere requires both the **API** and **MVC** applications to run concurrently.

### Option A: Using Visual Studio 2022 (Recommended)
1. Open `ShopSphere.sln` in Visual Studio 2022.
2. The solution is already configured with a multi-startup launch profile (`ShopSphere.slnLaunch.user`).
3. Press **F5** or click **Start**.
4. Visual Studio will launch:
   - `ShopSphere.API` on `http://localhost:5062` (Swagger opens automatically at `http://localhost:5062/swagger`).
   - `ShopSphere.MVC` on `http://localhost:5107` (The store frontend opens in your browser).

### Option B: Using .NET CLI

Launch both projects across two separate terminal sessions:

#### Terminal 1 — Start the API:
```bash
dotnet run --project ShopSphere.API --launch-profile http
```
* **API Address**: `http://localhost:5062`
* **Swagger UI**: `http://localhost:5062/swagger`

#### Terminal 2 — Start the MVC Frontend:
```bash
dotnet run --project ShopSphere.MVC --launch-profile http
```
* **MVC Web Application**: `http://localhost:5107`

---

## 4. Environment Variables & App Configuration

### Key Settings in `ShopSphere.API/appsettings.json`:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=ShopSphereDb;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False"
  },
  "Jwt": {
    "Key": "<Secret_Key_At_Least_32_Characters>",
    "Issuer": "ShopSphereAPI",
    "Audience": "ShopSphereClient",
    "DurationInDays": 30
  },
  "Stripe": {
    "SecretKey": "sk_test_PLACEHOLDER",
    "PublishableKey": "pk_test_PLACEHOLDER",
    "WebhookSecret": "whsec_PLACEHOLDER"
  },
  "AppUrl": "https://localhost:7002"
}
```

### Key Settings in `ShopSphere.MVC/appsettings.json`:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=ShopSphereDb;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False"
  }
}
```

> [!IMPORTANT]
> The MVC client communicates with the API at `http://localhost:5062/api/` (defined in `ShopSphere.MVC/Services/ApiService.cs`). If running on a different port or in a containerized environment, ensure the API URL aligns.

---

## 5. Logging Configuration

Both projects utilize **Serilog** configured in their respective `Program.cs` files:
* Console sink for immediate local debugging.
* Rolling daily file sink:
  * API logs: written to `logs/api-log-.txt` in the API root directory.
  * MVC logs: written to `logs/mvc-log-.txt` in the MVC root directory.

---

## 6. Production Deployment Recommendations

When deploying to a production server (IIS, Azure App Service, Linux Kestrel + Nginx, or Docker):
1. **Connection Strings**: Store connection strings in Azure Key Vault or server environment variables (`ConnectionStrings__DefaultConnection`).
2. **Reverse Proxy & HTTPS**: Enable HTTPS redirection, configure SSL/TLS certificates, and update `ApiService` base address to the secure production API URL.
3. **Data Protection & Cookie Keys**: In multi-instance deployments, configure ASP.NET Core Data Protection to store cryptographic keys in Redis or a shared network path to preserve user sessions.
4. **Environment Mode**: Ensure `ASPNETCORE_ENVIRONMENT=Production` to prevent Swagger UI and stack trace exposure.
