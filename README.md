# Blogera - ASP.NET MVC Blogging Platform

**Blogera** is a modern blogging platform built using ASP.NET MVC. It allows users to register, log in, create, and manage blog posts with rich UI and real-time features.

## ✨ Features

- 🧑‍💼 User Registration & Authentication (ASP.NET Identity)
- 📝 Blog Creation, Deletion
- 🔍 Blog Search & Viewing
- 👥 Role-Based Access (User/Admin)
- 📡 Real-time Updates using SignalR
- 🎨 Responsive UI with Tailwind CSS
- 🐳 Dockerized for easy deployment

## 🚀 Technologies Used

- ASP.NET MVC (.NET 6+)
- ASP.NET Identity
- SignalR
- Tailwind CSS
- Entity Framework
- MySQL
- Docker

## 🐳 Docker Image

You can pull and run the pre-built Docker image:

```bash
docker pull furqanahmad03/blogera
````

Run the container:

```bash
docker run -d -p 8080:80 furqanahmad03/blogera
```

> Visit `http://localhost:8080` to access the app.

## ⚙️ Setup Instructions (Manual)

> If you prefer running it locally without Docker:

1. **Clone the repository**

   ```bash
   git clone https://github.com/furqanahmad03/blogera.git
   cd blogera
   ```

2. **Update `appsettings.json`**

   Set your database connection string and other environment settings.

3. **Run the application**

   ```bash
   dotnet build
   dotnet run
   ```

## 📂 Project Structure

* `Controllers/` – MVC controllers
* `Models/` – Application models
* `Views/` – Razor views
* `wwwroot/` – Static files (Tailwind CSS, JS)
* `Hubs/` – SignalR hubs
* `Dockerfile` – Docker configuration

## 🛡️ License

This project is open-source and available under the MIT License.
