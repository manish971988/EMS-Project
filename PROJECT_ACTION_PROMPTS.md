# EMS Project Setup & Action Prompts Guide

This file provides detailed instructions and ready-to-use prompts for every major action required to set up and use the EMS Project from scratch.

---

## Prerequisites

- Install [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Install [Node.js (v18+ recommended)](https://nodejs.org/)
- Install [npm](https://www.npmjs.com/)
- Install MySQL Server and create a database for EMS

---

## Project Structure

- `API/` - .NET API backend
- `UI/` - Vue.js frontend

---

## Backend Setup (API)

### 1. Navigate to the API folder
**Prompt:**
```powershell
cd API
```

### 2. Restore NuGet packages
**Prompt:**
```powershell
dotnet restore
```

### 3. Configure Database Connection
**Prompt:**
- Open `API/appsettings.json` and set your MySQL connection string under `ConnectionStrings:DefaultConnection`.

### 4. Apply Entity Framework migrations
**Prompt:**
```powershell
dotnet ef database update
```

### 5. Run the API
**Prompt:**
```powershell
dotnet run
```
- The API will start on the port specified in `appsettings.json` or `Properties/launchSettings.json` (default: `http://localhost:5272`).

---

## Frontend Setup (UI)

### 1. Navigate to the UI folder
**Prompt:**
```powershell
cd UI
```

### 2. Install dependencies
**Prompt:**
```powershell
npm install
```

### 3. Run the development server
**Prompt:**
```powershell
npm run dev
```
- The app will be available at `http://localhost:5173`.

### 4. API Proxy Configuration
**Prompt:**
- Ensure `UI/vite.config.js` proxies `/api` requests to the backend (`http://localhost:5272`).

---

## Implemented Functionality & Usage Prompts

### Employee Management (API)

- **Add Employee**
  **Prompt:**
  ```http
  POST /api/employees
  Content-Type: application/json
  {
    "name": "John Doe",
    "email": "john@example.com",
    "roleId": 1,
    ...other fields...
  }
  ```

- **Edit Employee**
  **Prompt:**
  ```http
  PUT /api/employees/{id}
  Content-Type: application/json
  {
    "name": "Jane Doe",
    ...other fields...
  }
  ```

- **Soft Delete Employee**
  **Prompt:**
  ```http
  PUT /api/employees/{id}/delete
  ```

- **Get Employees**
  **Prompt:**
  ```http
  GET /api/employees
  ```

- **Get Employee by ID**
  **Prompt:**
  ```http
  GET /api/employees/{id}
  ```

### Employee Management (UI)

- **View Employee List:** Navigate to Employee List page in the UI.
- **Add Employee:** Click "Add Employee" and fill out the form.
- **Edit Employee:** Click "Edit" on an employee row and update details.
- **Delete Employee:** Click "Delete" on an employee row.

---

## Useful Commands

### API
- Build: `dotnet build`
- Run: `dotnet run`
- Migrate DB: `dotnet ef database update`

### UI
- Install deps: `npm install`
- Run dev: `npm run dev`
- Build: `npm run build`

---

## Troubleshooting Prompts

- **Check .NET version:**
  ```powershell
dotnet --version
```
- **Check Node.js version:**
  ```powershell
node --version
```
- **Check npm version:**
  ```powershell
npm --version
```
- **Check API errors:** Review terminal output and configuration files.
- **Check UI errors:** Review browser console and terminal output.

---

## Additional Notes

- API configuration: `API/appsettings.json`, `API/appsettings.Development.json`
- UI configuration: `UI/vite.config.js`
- Entity Framework migrations: `API/Migrations/`
- For further help, consult the README files or contact the project maintainer.
