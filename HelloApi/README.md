# HelloApi Project

## Overview
HelloApi is a .NET 9.0 API project that implements a CRUD interface for managing TPerson entities. The project utilizes Entity Framework Core with a SQLite database in a database-first approach and includes Swagger documentation for easy API exploration.

## Project Structure
The project is organized into the following directories and files:

- **Controllers**: Contains API controllers for different versions.
  - **V1/TPersonController.cs**: Implements CRUD operations for TPerson.
  - **V2/TPersonController.cs**: Enhancements or changes to the CRUD operations.
  
- **Models**: Contains the domain model.
  - **TPerson.cs**: Defines the TPerson entity with properties id, Nom, Prenom, DateBorn, and DateDead.
  
- **Data**: Contains the database context.
  - **HelloApiContext.cs**: Inherits from DbContext and manages TPerson entity interactions with the database.
  
- **Migrations**: Contains migration files for database schema changes.
  
- **Properties**: Contains application properties.
  - **launchSettings.json**: Configuration for launching the application.
  
- **appsettings.json**: Configuration settings, including connection strings.
  
- **Program.cs**: Entry point of the application, sets up the web host and services.
  
- **Startup.cs**: Configures services and middleware, including Swagger.
  
- **HelloApi.csproj**: Project file with metadata and dependencies.

## Setup Instructions
1. Clone the repository or download the project files.
2. Ensure you have .NET 9.0 SDK installed on your machine.
3. Navigate to the project directory in your terminal.
4. Restore the project dependencies by running:
   ```
   dotnet restore
   ```
5. Update the connection string in `appsettings.json` to point to your SQLite database.
6. Run the migrations to set up the database schema:
   ```
   dotnet ef database update
   ```
7. Start the application:
   ```
   dotnet run
   ```
8. Access the API documentation at `/swagger` to explore the available endpoints.

## Usage
- Use the API endpoints defined in the controllers to perform CRUD operations on TPerson entities.
- Refer to the Swagger documentation for detailed information on each endpoint and its parameters.

## Contributing
Contributions are welcome! Please submit a pull request or open an issue for any enhancements or bug fixes.




##CMD Instructions

To create and run the HelloApi project, follow these command-line instructions:

docker compose down
docker compose up -d --build
docker compose restart helloapi

docker exec -it helloapi_api_1 bash

dotnet new webapi -n HelloApi

cd HelloApi

Remove-Item .\certs\helloapi.pfx -Force
dotnet dev-certs https --trust
dotnet dev-certs https -ep "$PWD\certs\helloapi.pfx" -p "HelloApi!2025"


dotnet add package Microsoft.EntityFrameworkCore.Sqlite

dotnet add package Microsoft.EntityFrameworkCore.Design

dotnet add package Swashbuckle.AspNetCore

dotnet add package Microsoft.AspNetCore.Mvc.Versioning

dotnet add package Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer

dotnet add package Microsoft.EntityFrameworkCore.Tools

dotnet ef migrations add InitialCreate

dotnet ef database update
dotnet run
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
	
