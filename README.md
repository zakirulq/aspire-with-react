# Task Management App with .NET Aspire

A modern task management application built with **React TypeScript** frontend, **.NET 8** backend API, and **MongoDB** database, all orchestrated using **.NET Aspire** for a seamless development experience.

## 🚀 Features

### Frontend (React + TypeScript)
- **Task Management**: Create, read, update, and delete tasks
- **Real-time Search**: Search tasks by title or description
- **Status Filtering**: Filter by All, Pending, or Completed tasks
- **Responsive Design**: Modern UI with clean, intuitive interface
- **Task Details**: Detailed view with full task information
- **Auto-refresh**: Automatic updates when tasks are modified

### Backend (.NET 8 API)
- **RESTful API**: Full CRUD operations for tasks
- **MongoDB Integration**: NoSQL database for flexible data storage
- **CORS Support**: Configured for React frontend
- **Swagger Documentation**: API documentation at `/swagger`

### Infrastructure (.NET Aspire)
- **Container Orchestration**: MongoDB running in container
- **Service Discovery**: Automatic service communication
- **Development Dashboard**: Visual monitoring of all services
- **Port Management**: Automatic port allocation and exposure

## 🏗️ Architecture

```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   React App     │    │   .NET API      │    │   MongoDB       │
│   (Port 3000)   │◄──►│   (Port 5469)   │◄──►│   (Port 27017)  │
│                 │    │                 │    │                 │
│ - Task List     │    │ - TaskController│    │ - TaskDb        │
│ - Search        │    │ - TaskService   │    │ - Collections   │
│ - CRUD Ops      │    │ - CORS Config   │    │                 │
└─────────────────┘    └─────────────────┘    └─────────────────┘
         ▲                       ▲                       ▲
         │                       │                       │
         └───────────────────────┼───────────────────────┘
                                 │
                    ┌─────────────────┐
                    │   .NET Aspire   │
                    │   AppHost       │
                    │                 │
                    │ - Orchestration │
                    │ - Service Mgmt  │
                    │ - Dashboard     │
                    └─────────────────┘
```

## 📋 Prerequisites

Before running this project, ensure you have the following installed:

- **.NET 8 SDK** - [Download here](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Node.js 18+** - [Download here](https://nodejs.org/)
- **Git** - [Download here](https://git-scm.com/)
- **Docker Desktop** (optional, for MongoDB container) - [Download here](https://www.docker.com/products/docker-desktop/)

## 🛠️ Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/zakirulq/aspire-with-react.git
cd aspire-with-react
```

### 2. Install Dependencies

#### Backend Dependencies
```bash
# Restore .NET packages
dotnet restore
```

#### Frontend Dependencies
```bash
# Navigate to React app directory
cd task-management-client

# Install Node.js dependencies
npm install

# Return to root directory
cd ..
```

### 3. Configure Environment

#### API Configuration
The API is configured to run on port `5469` by default. If you need to change this:

1. Update `TaskManagementApp.ApiService/Properties/launchSettings.json`
2. Update `task-management-client/src/api.ts` with the new port

#### React App Configuration
The React app is configured to connect to the API at `http://localhost:5469`. If you change the API port, update:

```typescript
// task-management-client/src/api.ts
const API_BASE_URL = process.env.REACT_APP_API_BASE_URL || 'http://localhost:5469';
```

## 🚀 Running the Application

### Option 1: Using .NET Aspire (Recommended)

This is the easiest way to run the entire application stack:

```bash
# From the root directory
dotnet run --project TaskManagementApp.AppHost
```

This will:
- Start MongoDB container
- Launch the .NET API service
- Start the React development server
- Open the Aspire dashboard
- Provide service URLs in the console

### Option 2: Running Services Individually

#### Start MongoDB
```bash
# Using Docker
docker run -d --name mongodb -p 27017:27017 mongo

# Or using MongoDB locally if installed
mongod
```

#### Start the API
```bash
# From root directory
dotnet run --project TaskManagementApp.ApiService
```

#### Start the React App
```bash
# From root directory
cd task-management-client
npm start
```

## 🌐 Accessing the Application

After starting the application:

- **React Frontend**: http://localhost:3000
- **API Documentation**: http://localhost:5469/swagger
- **Aspire Dashboard**: http://localhost:7000 (if using Aspire)

## 📱 Using the Application

### Task Management
1. **View Tasks**: See all tasks on the main page
2. **Search**: Use the search box to find tasks by title or description
3. **Filter**: Use status buttons to filter by All, Pending, or Completed
4. **Add Task**: Click "Add New Task" to create a new task
5. **Edit Task**: Click on any task to view details and edit
6. **Delete Task**: Use the delete button on task cards or detail pages

### API Endpoints
- `GET /tasks` - Get all tasks
- `GET /tasks/{id}` - Get specific task
- `POST /tasks` - Create new task
- `PUT /tasks/{id}` - Update task
- `DELETE /tasks/{id}` - Delete task

## 🏗️ Project Structure

```
aspire-with-react/
├── TaskManagementApp.ApiService/     # .NET 8 API
│   ├── Controllers/
│   │   ├── Models/
│   │   ├── Services/
│   │   └── Program.cs
│   ├── TaskManagementApp.AppHost/        # Aspire orchestration
│   └── Program.cs
├── TaskManagementApp.ServiceDefaults/ # Shared services
├── task-management-client/           # React TypeScript app
│   ├── src/
│   │   ├── components/              # React components
│   │   ├── context/                 # React context
│   │   ├── hooks/                   # Custom hooks
│   │   └── api.ts                   # API client
│   └── package.json
└── README.md
```

## 🔧 Development

### Adding New Features
1. **Backend**: Add controllers and services in the API project
2. **Frontend**: Create new components in `task-management-client/src/components/`
3. **Database**: MongoDB collections are created automatically

### Code Organization
- **Components**: Modular React components for reusability
- **Context**: Global state management with React Context
- **Hooks**: Custom hooks for business logic
- **API**: Centralized API client with TypeScript interfaces

## 🐛 Troubleshooting

### Common Issues

#### Port Already in Use
```bash
# Find process using port
lsof -i :3000  # For React
lsof -i :5469  # For API
lsof -i :27017 # For MongoDB

# Kill process
kill -9 <PID>
```

#### MongoDB Connection Issues
- Ensure MongoDB is running
- Check if port 27017 is available
- Verify connection string in API configuration

#### React Build Issues
```bash
# Clear npm cache
npm cache clean --force

# Reinstall dependencies
rm -rf node_modules package-lock.json
npm install
```

#### .NET Build Issues
```bash
# Clean and rebuild
dotnet clean
dotnet build
```

## 📝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🤝 Support

If you encounter any issues or have questions:

1. Check the [Issues](https://github.com/zakirulq/aspire-with-react/issues) page
2. Create a new issue with detailed information
3. Include error messages and steps to reproduce

---

**Happy Coding! 🚀**
