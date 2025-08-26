🛒 E-Commerce Web App
📌 Overview

A full-stack E-Commerce web application built with ASP.NET Core MVC using a clean architecture approach.
The app allows users to:

Browse products & categories

Add items to cart (AJAX based)

Place & manage orders

View & update profile information

Benefit from advanced features such as authentication, role-based authorization, and order tracking.

This project was a new business idea and a complete end-to-end implementation.

🏗️ Architecture & Design

ASP.NET Core MVC

N-Tier Architecture (Presentation Layer, BLL, DAL, Entities)

Repository Pattern

SOLID Principles & Clean Code

Reusable Components

EF Core (Code-First) with LINQ queries

🎨 Frontend

HTML, CSS, Bootstrap (UI templates improved/customized using AI tools)

Partial Views & Tag Helpers for reusable UI components

JavaScript + Fetch API for AJAX calls (cart updates, search, pagination)

Responsive and modern UI/UX

⚙️ Backend

CRUD Operations for products, categories, users, orders

Admin Panel (Dashboard) for managing content & analytics

Sales statistics, orders, and users count displayed using charts (data retrieved with LINQ queries)

ASP.NET Identity for authentication & authorization

User registration, login, update password

Password reset via email (SMTP API Key – Brevo)

Role-based access control

🚀 Features

🛒 Shopping Cart Management (Add / Update / Remove) with AJAX

🍪 Cookies used to persist cart items across sessions

⚡ Optimized user experience by reducing full-page reloads

✅ Validation

Client-side & server-side (Data Annotations, Remote Validation)

📂 File Upload (product images) with type & size validation + secured upload process

🔍 Product Search & Pagination (AJAX-based)

📊 Dashboard Analytics (charts integrated with live DB queries)

🔧 Tech Stack

Backend: ASP.NET Core MVC, EF Core, LINQ

Frontend: HTML, CSS, Bootstrap, JavaScript, AJAX (Fetch API)

Database: SQL Server (Code-First Migrations)

Identity & Security: ASP.NET Identity, Role-Based Authorization, Brevo Email API

Tools & Practices: Repository Pattern, SOLID, Clean Code, N-Tier Architecture
