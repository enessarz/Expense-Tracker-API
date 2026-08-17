# 💰 Expense Tracker API

A robust, RESTful Web API built with **ASP.NET Core** and **C#** for managing personal finances. This API allows users to register, log in securely, and perform full CRUD (Create, Read, Update, Delete) operations on their personal expenses.

## ✨ Features

*   **Secure Authentication:** User registration and login using JWT (JSON Web Tokens).
*   **Password Hashing:** Passwords are safely hashed before entering the database using BCrypt.
*   **Data Isolation:** Users can only access, view, and modify their own personal expense records.
*   **Entity Framework Core:** Object-Relational Mapping (ORM) integrated with a lightweight SQLite database for fast local development.
*   **Fully Documented Endpoints:** Standardized routing and HTTP methods for easy client integration.

## 🛠️ Tech Stack

*   **Framework:** ASP.NET Core Web API (.NET)
*   **Language:** C#
*   **Database:** SQLite
*   **ORM:** Entity Framework Core
*   **Security:** BCrypt.Net, JWT Bearer Authentication

---

## ⚠️ Important Setup Note
For security reasons, `appsettings.json` and the SQLite database are not included in this repository. 

To run this project locally:
1. Create an `appsettings.json` file in the root directory and add your `"JwtSettings"` (Secretkey, Issuer, and Audience).
2. Run `dotnet ef database update` in your terminal to generate the local SQLite database.