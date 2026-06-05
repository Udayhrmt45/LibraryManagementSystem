# Library Management System

A RESTful ASP.NET Core Web API for managing books, authors, members, publishers, categories, and book issue transactions.

## Technologies Used

- ASP.NET Core 10
- Entity Framework Core
- SQL Server
- JWT Authentication
- Role Based Authorization (RBAC)
- Swagger 
- AutoMapper
- Dependency Injection

---

## Features

### Author Management
- Create Author
- Update Author
- Delete Author (Soft Delete)
- Get Author By Id
- Get All Authors

### Book Management
- Create Book
- Update Book
- Delete Book
- Get Book Details

### Member Management
- Register Members
- Update Members
- View Member Information

### Book Issue Management
- Issue Books
- Return Books
- View Issued Books

### Security
- JWT Authentication
- Role-Based Authorization
- Password Hashing

### Advanced Features
- UUID Based APIs
- Pagination
- Soft Delete
- Global Exception Handling Middleware
- Centralized Error Logging
- Swagger Documentation

---

## Project Structure

```text
LibraryManagementSystem.API
LibraryManagementSystem.Common
LibraryManagementSystem.Service
LibraryManagementSystem.Store
```

---

## Database

SQL Server

Tables include:

- mst_Authors
- mst_Books
- mst_Members
- mst_Roles
- mst_Users
- mst_Categories
- tbl_BookIssues
- tbl_Fines
- tbl_ErrorLogs

---

## API Documentation

Swagger is enabled.

Run the application and navigate to:

https://localhost:{port}/swagger

---

## Setup Instructions

1. Clone Repository

```bash
git clone https://github.com/Udayhrmt45/LibraryManagementSystem.git
```

2. Update Connection String

appsettings.json

3. Run Database Scripts

4. Run Application

```bash
dotnet run
```

---

## Author

Uday Hiremath
