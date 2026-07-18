# EmployeeManagementSystem

**A desktop-based enterprise workforce directory application designed to manage employee registries, department allocation, salary tracking, and credentials using C#, .NET Windows Forms, and SQL Server database systems.**

[![C#](https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![.NET Framework](https://img.shields.io/badge/.NET_Framework-4.8-512BD4?style=for-the-badge&logo=.net&logoColor=white)](https://dotnet.microsoft.com/)
[![Microsoft SQL Server](https://img.shields.io/badge/SQL_Server-2022-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)](https://www.microsoft.com/en-us/sql-server/)

---

## Table of Contents
- [Overview](#overview)
- [Key Features](#key-features)
- [Architecture](#architecture)
- [Database Structure](#database-structure)
- [Project Directory Structure](#project-directory-structure)
- [Getting Started](#getting-started)
- [Database Configuration](#database-configuration)
- [Engineering Highlights](#engineering-highlights)

---

## Overview

The **EmployeeManagementSystem** is an administrative workforce directory software developed in C# utilizing .NET Windows Forms. The platform acts as a secure console for human resources staff, enabling seamless tracking of staff information, department hierarchies, role distributions, salary grades, and account authentication records.

---

## Key Features

### User Session Authentication
- Login and registration portals to secure directory data.
- Built-in credentials verification utilizing ADO.NET query flows.

### Employee Directory Management
- Full Create, Read, Update, and Delete (CRUD) operations for employee profiles.
- Fields tracking employee names, email addresses, contact directories, hiring dates, and active titles.

### Organizational and Salary Mapping
- Dynamic allocation of employees to active corporate departments.
- Base salary settings and compensation parameters tracker.

### Reporting Dashboard
- Aggregated metadata reporting active staff counts, salary distributions, and department statistics directly in a consolidated main page.

---

## Architecture

The system runs on a traditional two-tier desktop architecture:

```
Presentation Interface (WinForms) <---> Connection Manager (DatabaseHandler) <---> SQL Database
```

- **Presentation Interface**: Native Windows Forms controls displaying grids, inputs, and state changes.
- **Connection Manager**: Centralized in `DatabaseHandler.cs` using SQL Client tools (`SqlConnection`, `SqlCommand`, `SqlDataReader`) to bind datasets.

---

## Database Structure

The project includes `EmployeeManagemetSQL.sql` containing schema structures:

- **Users Table**: Security records containing username and password credentials.
- **Employees Table**: Demographics, contact information, department indices, titles, and salary data.

---

## Project Directory Structure

```
EmployeeManagementSystem_Project/
├── EmployeeManagement.sln               # Visual Studio Solution Configuration
└── EmployeeManagement/
    ├── App.config                        # Configuration mapping connection strings
    ├── Program.cs                        # Main execution bootstrap file
    ├── DatabaseHandler.cs                # Core ADO.NET connection interface
    ├── EmployeeManagemetSQL.sql          # SQL schema generation script
    ├── Login.cs                          # Authentication portal
    ├── Register.cs                       # Management registry window
    ├── Dashboard.cs                      # Primary analytics console
    ├── EmployeeManagement.cs             # Employee directory management interface
    └── Properties/                       # Metadata and settings configs
```

---

## Getting Started

### Prerequisites
- **Visual Studio 2022** (with .NET Desktop Development tools)
- **.NET Framework 4.8**
- **Microsoft SQL Server**

### Local Configuration
1. Clone this repository:
   ```bash
   git clone https://github.com/aykutern/EmployeeManagementSystem_Project.git
   ```
2. Open `EmployeeManagement.sln` inside Visual Studio.
3. Configure the Database Connection string within `App.config` to match your local SQL Server instance name.
4. Press `Ctrl + Shift + B` to build and `F5` to start debugging.

---

## Database Configuration

Prepare the SQL Server instance by executing the script in `EmployeeManagemetSQL.sql`:

```sql
-- Initializes database structures
CREATE DATABASE EmployeeDb;
USE EmployeeDb;

-- Table definitions map automatically via the script content
```

---

## Engineering Highlights

- **Parametrization Validation**: Prevents SQL injection by sanitizing inputs using typed ADO.NET variables.
- **Dynamic DataGridView Binding**: Binds query responses to data grids dynamically, ensuring instant visual state changes upon database updates.
- **Resource Lifecycle Stewardship**: Safely disposes database connection elements to ensure stability in desktop runtimes.
