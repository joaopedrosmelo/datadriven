# Data Driven Api

This project consists of two main parts: a backend API and a background service. The project is built with .NET Core 8.

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0 or higher)

## Db Configuration

1. **Connection String**:
   The connection string setted is ready to working when run the application.
   But if you need to use your own just update on the appsettings "DefaultConnection" and run the migration commands below:
    ```bash
   Add-Migration InitialCreate
   Update-Database

## Running the Backend API

1. **Navigate to the Backend Project Directory**:
   ```bash
   cd path/to/DataDriven/project

2. **Restore NuGet Packages**:
   dotnet restore

3. **Build the Project**:
   dotnet build

4. **Run the API**:
   dotnet run

The API should now be running on https://localhost:44300/api.
