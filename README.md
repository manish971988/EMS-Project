# EMS Project

This repository contains the EMS (Employee Management System) project, which includes a .NET API backend and a Vue.js frontend UI.

## Table of Contents
- [Project Structure](#project-structure)
- [Prerequisites](#prerequisites)
- [Setup Instructions](#setup-instructions)
- [Useful Commands](#useful-commands)
- [Troubleshooting](#troubleshooting)
- [Additional Notes](#additional-notes)

---

## Project Structure

- `API/` - .NET API backend
- `UI/` - Vue.js frontend

---

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Node.js (v18+ recommended)](https://nodejs.org/)
- [npm](https://www.npmjs.com/)

---

## Setup Instructions

### Backend Setup (API)

1. Navigate to the API folder:
   ```powershell
   cd API
   ```
2. Restore NuGet packages:
   ```powershell
   dotnet restore
   ```
3. Apply Entity Framework migrations (optional, if using a database):
   ```powershell
   dotnet ef database update
   ```
4. Run the API:
   ```powershell
   dotnet run
   ```
   The API will start on the port specified in `appsettings.json` or `launchSettings.json`.

### Frontend Setup (UI)

1. Navigate to the UI folder:
   ```powershell
   cd UI
   ```
2. Install dependencies:
   ```powershell
   npm install
   ```
3. Run the development server:
   ```powershell
   npm run dev
   ```
   The app will be available at `http://localhost:5173` (default Vite port).

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

## Troubleshooting

- If you encounter issues with dependencies, ensure you have the correct versions of .NET and Node.js installed.
- For API errors, check the output in the terminal and review configuration files.
- For UI errors, check the browser console and terminal output.

---

## Additional Notes

- **Configuration:**
  - API configuration files: `API/appsettings.json`, `API/appsettings.Development.json`
  - Frontend config: `UI/vite.config.js`
- **Database:**
  - Ensure your database connection string is set correctly in `appsettings.json`.
- **Migrations:**
  - Entity Framework migrations are in `API/Migrations/`.

---

For further help, consult the README files in each project folder or contact the project maintainer.
