# Troubleshooting & Common Issues Guide

This guide assists developers in identifying, diagnosing, and resolving common errors encountered during local development, database configuration, or application runtime.

---

## 1. Database & SQL Server LocalDB Issues

### Issue: "A network-related or instance-specific error occurred while establishing a connection to SQL Server"
* **Symptom**: Application fails on startup or when executing database queries with `SqlException: Cannot open database "ShopSphereDb"`.
* **Root Cause**: The SQL Server LocalDB instance `MSSQLLocalDB` is stopped or not initialized on Windows.
* **Resolution**:
  1. Open PowerShell or Command Prompt.
  2. Verify the status of your LocalDB instances:
     ```powershell
     sqllocaldb info
     ```
  3. Start the default instance:
     ```powershell
     sqllocaldb start MSSQLLocalDB
     ```
  4. If the instance is missing or corrupted, recreate it:
     ```powershell
     sqllocaldb delete MSSQLLocalDB
     sqllocaldb create MSSQLLocalDB
     sqllocaldb start MSSQLLocalDB
     ```
  5. Apply migrations:
     ```powershell
     dotnet ef database update --project ShopSphere.Infrastructure --startup-project ShopSphere.API
     ```

### Issue: "Invalid object name 'Products'" or "Invalid object name 'Categories'"
* **Root Cause**: Migrations have not been applied to `ShopSphereDb`.
* **Resolution**:
  Execute the database update command from the repository root:
  ```powershell
  dotnet ef database update --project ShopSphere.Infrastructure --startup-project ShopSphere.API
  ```

---

## 2. API & MVC Inter-Service Communication Issues

### Issue: MVC Frontend loads but shows empty product lists or zero categories
* **Symptom**: The homepage or products catalog shows empty tables, and `AdminController` displays error messages ("فشل في حفظ المنتج بالـ API").
* **Root Cause**: `ShopSphere.API` is either not running, or running on a port different from `http://localhost:5062`.
* **Resolution**:
  1. Verify that `ShopSphere.API` is actively running:
     ```powershell
     curl http://localhost:5062/api/products
     ```
  2. Open Swagger UI in your browser: `http://localhost:5062/swagger`. If Swagger fails to open, start the API project:
     ```powershell
     dotnet run --project ShopSphere.API --launch-profile http
     ```
  3. Verify `ShopSphere.MVC/Services/ApiService.cs` line 13:
     ```csharp
     private readonly string _apiBaseUrl = "http://localhost:5062/api/";
     ```
     Ensure the port matches the active API HTTP endpoint.

---

## 3. Port Conflicts

### Issue: "Failed to bind to address http://localhost:5062: address already in use"
* **Root Cause**: A previous background instance of `dotnet` or another process is holding port 5062 or 5107.
* **Resolution**:
  1. Find the process using the port in Windows PowerShell:
     ```powershell
     Get-NetTCPConnection -LocalPort 5062, 5107 -ErrorAction SilentlyContinue | Select-Object LocalPort, OwningProcess
     ```
  2. Terminate the process by ID:
     ```powershell
     Stop-Process -Id <OwningProcessId> -Force
     ```

---

## 4. Log Inspection

Both `ShopSphere.API` and `ShopSphere.MVC` use **Serilog** configured to output to daily rolling log files:

* **API Server Logs**:
  * Location: `ShopSphere.API/logs/api-log-<YYYYMMDD>.txt`
  * Contains HTTP request lifecycle traces, exception stack traces, EF Core SQL queries, and error outputs.
* **MVC Web Logs**:
  * Location: `ShopSphere.MVC/logs/mvc-log-<YYYYMMDD>.txt`
  * Contains Razor rendering errors, session issues, authentication events, and failed HTTP calls to `ApiService`.

---

## 5. Entity Framework Core Tooling

### Issue: "dotnet : The term 'dotnet-ef' is not recognized"
* **Root Cause**: The EF Core CLI global tool is not installed on the machine.
* **Resolution**:
  Install or update the global tool:
  ```powershell
  dotnet tool install --global dotnet-ef
  ```
  If already installed, update to .NET 8:
  ```powershell
  dotnet tool update --global dotnet-ef
  ```
