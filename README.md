# 🛒 Order Management System API

An ASP.NET Core Web API project that allows customers to place and manage orders while giving administrators control over product management, order processing, and invoice generation.

## ✅ Features

- Customer registration & login (JWT Auth)
- Place and view orders
- Product catalog browsing
- Role-based access control (Admin / Customer)
- Automatic invoice generation
- Tiered discount system (5% for orders > $100, 10% for > $200)
- Inventory tracking
- Multiple payment methods (Credit Card, PayPal)
- Email notification system on order status change
- Swagger API Documentation

---

## 📦 Technologies Used

- ASP.NET Core Web API
- Entity Framework Core (In-Memory DB)
- JWT Authentication
- Swagger (Swashbuckle)
- C#
- RESTful API
- Dependency Injection

---

## 🛠 Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/OrderManagementSystem.git
cd OrderManagementSystem
```

### 2. Run the Application

```bash
dotnet restore
dotnet dev-certs https --trust
dotnet run
```

### 3. Open Swagger

Visit:  
```
https://localhost:8295/swagger
```

---

## 🧪 Sample Credentials

### 👤 Admin User
```json
{
  "username": "admin",
  "password": "admin123"
}
```

### 👤 Customer User
```json
{
  "username": "customer",
  "password": "customer123"
}
```

---

## 🔐 Authentication

- Use `/api/users/login` to get a **JWT token**.
- Pass the token in the `Authorization` header like:

```
Authorization: Bearer {your_token}
```

---

## 📌 Sample Endpoints

### 👥 Customers
- `POST /api/customers`
- `GET /api/customers/{customerId}/orders`

### 📦 Orders
- `POST /api/orders`
- `GET /api/orders/{orderId}`
- `GET /api/orders` (admin only)
- `PUT /api/orders/{orderId}/status` (admin only)

### 📦 Products
- `GET /api/products`
- `GET /api/products/{productId}`
- `POST /api/products` (admin only)
- `PUT /api/products/{productId}` (admin only)

### 🧾 Invoices
- `GET /api/invoices/{invoiceId}` (admin only)
- `GET /api/invoices` (admin only)

### 👤 Users
- `POST /api/users/register`
- `POST /api/users/login`

---

## 📄 License

This project is for academic and learning purposes.
