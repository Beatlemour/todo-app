# Todo App

A simple full-stack TODO list application — view, add, and delete items.

## Stack

- **Backend:** .NET 10 Web API (C#), in-memory data store, xUnit tests
- **Frontend:** Angular 22 (standalone components, signals), plain CSS

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) (LTS) and npm
- Angular CLI (installed automatically via `npx` — no global install required)

## Project Structure

todo-app/
├── backend/ # .NET Web API
├── backend.tests/ # xUnit unit + integration tests
├── frontend/ # Angular app
└── README.md


## Running the Backend

```powershell
cd backend
dotnet run
```

The API starts on **http://localhost:5192**.

Endpoints:

| Method | Route            | Description       |
|--------|-------------------|--------------------|
| GET    | `/api/todo`       | List all todos     |
| POST   | `/api/todo`       | Add a todo (`{ "title": "..." }`) |
| DELETE | `/api/todo/{id}`  | Delete a todo by id |

## Running the Frontend

In a separate terminal:

```powershell
cd frontend
npm start
```

The app is served at **http://localhost:4200**.

> **Note:** the frontend calls the backend at `http://localhost:5192`. Both need to be running at the same time for the app to work. If you change the backend's port, update `apiUrl` in `frontend/src/app/services/todo.service.ts` to match, and update the CORS origin in `backend/Program.cs` if you change the frontend's port.

### Windows PowerShell users

If `npm`/`npx` commands fail with a "running scripts is disabled" error, run this once per terminal session before using npm:

```powershell
Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass
```

## Running Tests

Backend (unit + integration tests):

```powershell
cd backend.tests
dotnet test
```

## Architecture Notes

- The backend follows a layered structure: `Controllers` (HTTP/routing) → `Services` (business logic, in-memory store) → `Models` (domain entities), with the service injected via DI as a singleton so state persists across requests.
- The in-memory store uses `ConcurrentDictionary` for thread-safe access under concurrent requests.
- The frontend uses Angular's standalone component and signal-based reactivity (no NgModules, no Zone.js dependency for change detection triggers where avoidable).
- CORS is configured on the backend to allow requests only from `http://localhost:4200` (the Angular dev server).