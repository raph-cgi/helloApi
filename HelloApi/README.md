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