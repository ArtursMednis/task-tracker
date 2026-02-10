
# TaskTracker– Tracker for tasks and statuses

**TaskTracker** is a lightweight fullstack web application that helps you track and manage your Tasks with due dates and statuses. Built with modern technologies for both frontend and backend.

---

## Tech Stack

**Frontend:**

- Angular 21.1.3

**Backend:**

- .NET 9 Web API
- Entity Framework Core
- SQL Server
- Swagger OpenAPI

---

## Project Structure

```
TaskTracker/
│
├── backend/
│   ├── TaskTracker.Api/
│   │   └── (.NET REST web API)
│   ├── TaskTracker.Application/
│   │   └── (Contains the application logic)
│   ├── TaskTracker.Domain/
│   │   └── (Contains the domain models and business rules)
│   ├── TaskTracker.Identity/
│   │   └── (Contains authorization services)
│   ├── TaskTracker.Infrastructure/
│   │   └── (Contains system related services)
│   ├── TaskTracker.Persistance/
│   │   └── (Contains the DB access layer)
│   │
│   ├── DatabaseMigrations/
│   │   └── (SQL migration scripts to set up databases)
│   │
│   └── TaskTracker.sln
│
└── frontend/
    └── task-tracker/
        ├── src/
        ├── dist/
        ├── src/
        ├── angular.json
        └── (Angular frontend application)
```

---

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Node.js & npm](https://nodejs.org/)
- Angular CLI 21.1.3
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

---

### Backend Setup

1. **Set the database connection strings:**

The API expects 2 connection strings: one for Identity **TaskTrackerIdentityDatabase** and one for TaskTracking **TaskTrackerDatabase**

You can set them in:
`backend\TaskTracker.Api\appsettings.json`

2. **Run the SQL migration script:**

Before launching the API, execute both migartion scripts each in its own database to create the required schema:

`backend\DatabaseMigrations\TaskTrackerDb.sql`
`backend\DatabaseMigrations\TaskTrackerIdentityDb.sql`

3. **Run the backend API:**

```bash
cd backend\TaskTracker.Api
dotnet restore
dotnet run
```

4. **Verify the API Is Running:**

Open a browser and navigate to:

``` bash
https://localhost:7289/swagger/index.html
```

There you can access Swagger UI to explore the API endpoints.

The API also serves the **pre-built frontend** from its `wwwroot` folder — available at `http://localhost:5131`.

### Frontend Setup



1.  **Install dependencies:**

```bash
cd frontend\task-tracker
```

Install Angular if you haven't
```bash
npm install -g @angular/cli
```

Install other project dependencies
```
npm install
```

2. **Run the development server:**

```bash
ng serve
```

The app will start on `http://localhost:4200`
