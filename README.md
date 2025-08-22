# Employees Management System

## 📦 Project Overview

This repo includes:
* A .NET 9 Web API
* Clean Architecture with DDD principles
* CQRS pattern
* MediatR for command and query handling
* Swagger for API documentation
* FluentValidation for input validation
* CRUD operations for employees
* Geniric repository pattern
* Unit of Work pattern
* Migrations with EF Core
* Unit testing with xUnit
* Pagination

## 🚀 Getting Started

### 1. Clone the repo

```bash
git clone https://github.com/ZakariaBakhkhouch/employees-management-api.git
```

### 2. Build & Run with Docker Compose

```bash
docker-compose up --build
```

This will:
- Build the .NET API container
- Pull the official SQL Server image

### 3. Test the API

Visit:  
`http://localhost:8080/swagger/index.html`

### 🔑 SQL Server Settings

Environment variables for SQL Server are defined in `docker-compose.yml`:

```yaml
SA_PASSWORD: "Your_password123"
ACCEPT_EULA: "Y"
```

Update the connection string in the `.NET API` accordingly:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=sqlserver;Database=MyAppDb;User=sa;Password=Your_password123;"
}
```