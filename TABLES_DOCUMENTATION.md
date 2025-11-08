# EMS Tables Documentation

This document describes the tables available in the Employee Management System (EMS).

## Available Tables

### 1. Employee Table
**Route:** `/employees` or `/`  
**Description:** Displays all active and inactive employees in the system.

**Features:**
- View employee details (Name, Department, Designation, Salary, Status)
- Search and filter by name, department, or designation
- Filter by department
- Add new employees
- Edit existing employees
- Soft delete employees (Admin only)
- Responsive design for mobile devices

**API Endpoints:**
- `GET /api/employees` - List all employees
- `GET /api/employees/{id}` - Get specific employee
- `POST /api/employees` - Create new employee
- `PUT /api/employees/{id}` - Update employee
- `PUT /api/employees/{id}/delete` - Soft delete employee

### 2. Users Table
**Route:** `/users`  
**Description:** Displays all system users with their associated roles.

**Features:**
- View user details (ID, Name, Email, Role, Created Date)
- See which role each user is assigned to
- Responsive table design

**API Endpoints:**
- `GET /api/users` - List all users with their roles
- `GET /api/users/{id}` - Get specific user

### 3. Roles Table
**Route:** `/roles`  
**Description:** Displays all available roles in the system.

**Features:**
- View role details (ID, Role Name)
- Simple, clean table layout

**API Endpoints:**
- `GET /api/roles` - List all roles
- `GET /api/roles/{id}` - Get specific role

## Navigation

The application includes a navigation menu at the top of the page with links to all three tables:
- **Employees** - Main employee management
- **Users** - System user management
- **Roles** - Role management

## Database Schema

All tables are backed by MySQL database with the following relationships:
- `Employees` → `Users` (CreatedBy, UpdatedBy foreign keys)
- `Users` → `Roles` (RoleId foreign key)

## Access Considerations

Currently, all endpoints are accessible without authentication. In a production environment, you should:
1. Enable authentication/authorization
2. Restrict access to Users and Roles tables to Admin users only
3. Implement proper role-based access control (RBAC)
