# Branches Management System

## Overview
Branches Management System is a web application for managing multiple branches, employees, and maintenance requests. It provides authentication, role-based access control, and pagination features.

## Features
- User Authentication (JWT-based login/logout)
- Role-Based Access Control (Admin & Employee roles)
- Manage Employees & Branches
- Secure API with .NET Core
- UI Integration: A clean and user-friendly interface.
- Error Handling: Comprehensive error handling for API requests and UI operations.


## Technologies Used
### Backend (API)
- **.NET Core Web API**
- **Entity Framework Core (Code-First Approach)**
- **ASP.NET Core Identity** (Authentication & Authorization)
- **SQL Server** (Database)
- **JWT (JSON Web Token)**
- **REST API**

### Frontend (BM_Web)
- **ASP.NET Core MVC**
- **Bootstrap** (UI Framework)
- **HttpClient** (API calls)
- **Session & Cookies** (Authentication Management)

## Setup Instructions

### 1️⃣ Clone the Repository
```sh
git clone https://github.com/mehofwzy/BranchesManagementSystem.git
cd Branches-Management-System
```

### 2️⃣ Configure the Database
- Open `appsettings.json` in the API project and set the connection string for SQL Server.
- Run migrations:
```sh
dotnet ef database update
```

### 3️⃣ Run the API
```sh
cd BM_Api
 dotnet run
```

### 4️⃣ Run the Frontend (BM_Web)
```sh
cd BM_Web
 dotnet run
```

## API Endpoints
| Method | Endpoint | Description |
|--------|---------|-------------|
| POST   | `/api/auth/register` | Register a new user |
| POST   | `/api/auth/login` | Authenticate user and return JWT |
| GET    | `/api/employees?page=1&pageSize=10` | Get paginated employee list |
| GET    | `/api/branches` | Get all branches |

## Authentication & Authorization
- Users must log in to access the system.
- Only **Admin** users can create, edit, or delete employees.
- Employees can view their assigned branches.

## License

This project is made by Eng. Mohamed Fawzy - .NET Software Developer

## Contact Information

- Mohamed Fawzy
- +201095194149
- mehofawzy@outlook.com
- linkedin.com/in/mehofwzy

