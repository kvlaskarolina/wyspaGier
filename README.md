# QuickFun - Game Island Platform

A Blazor WebAssembly application for browser-based games, built with Clean Architecture and SOLID principles.

##  Quick Start

### Prerequisites

- [.NET 8.0 SDK or later](https://dotnet.microsoft.com/download)
- Git
- A code editor (VS Code, Visual Studio, or Rider)

### Installation

1. **Clone the repository**
```bash
git clone https://github.com/your-username/QuickFun.git
cd QuickFun
```

2. **Restore dependencies**
```bash
dotnet restore
```

3. **Build the solution**
```bash
dotnet build
```

4. **Run the application**
```bash
dotnet run --project QuickFun.Web
```

Or with hot-reload for development:
```bash
dotnet watch --project QuickFun.Web
```

5. **Open in browser**

Navigate to:
- `http://localhost:5000` or
- `https://localhost:5001`

The application should now be running! 

---

##  Project Structure
```
QuickFun/
├── QuickFun.Domain/          # Domain entities and interfaces
├── QuickFun.Application/     # Business logic and services
├── QuickFun.Infrastructure/  # Data access and external services
├── QuickFun.Web/            # Blazor WebAssembly frontend
├── QuickFun.Games/          # Game implementations
└── QuickFun.Tests/          # Unit and integration tests
```

---

##  Development

### Running tests
```bash
dotnet test
```

### Clean and rebuild
```bash
dotnet clean
dotnet build
```

### Run specific project
```bash
dotnet run --project QuickFun.Web
```

### Using Visual Studio Code
```bash
code .
```
Then press `F5` to run with debugging.

---

## Available Games

//to add

---

##  Architecture

This project follows:
- **Clean Architecture** - Separation of concerns across layers
- **SOLID Principles** - Maintainable and scalable code
- **Design Patterns** - Repository, Factory, Strategy, CQRS, Observer

---

## Technologies

- **Blazor WebAssembly** - Frontend framework
- **Entity Framework Core** - ORM
- **MediatR** - CQRS implementation
- **AutoMapper** - Object mapping
- **FluentValidation** - Input validation
- **xUnit** - Testing framework

---

##  Troubleshooting

### Port already in use
```bash
dotnet run --project QuickFun.Web --urls "http://localhost:5005"
```

### Build errors
```bash
dotnet clean
dotnet restore
dotnet build
```

### .NET SDK not found
Install from: https://dotnet.microsoft.com/download

---

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

---

## License

This project is licensed under the MIT License.

---

