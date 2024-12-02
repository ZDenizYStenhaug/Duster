Duster is designed as a microservice that is meant to be integrated into the Tibber Platform. It simulates a cleaning robot moving around an office space.

It is written in C# with .NET and uses Entity Framework Core to manage the database.

## Installation
The application can be run on either your local machine, or on docker.

### System Requirements
* [.Net 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

### Migrations
Database migrations are performed on startup.

### Run application locally
From the root directory of the project, run:
1. ```dotnet restore``` to fetch all required NuGet packages. 
2. Configure database connection by either editing the connection string in ```/Duster/appsettings.json``` or setting an environment variable.
For windows: 
``` $env:ConnectionStrings__db="Port=;Database=;Username=;Password="```

3. ```dotnet build``` to build the project
4. ```dotnet run --project .\Duster\``` to run the project

### Run application on docker
From the root directory of the project, run 
```docker compose up -d --build```

This will start up a PostgresSQL database and Duster application. The connection to the database is configured in compose.yaml file.

## Usage
Once the project is running, the swagger documentation is accessible on http://localhost:5000/swagger/index.html

## Notes
In project specification, the input below is given as an example: 
```
{
"start": {
"x": 10,
"y": 22 },
"commmands": [
{
"direction": "east",
"steps": 2 },
{
"direction": "north",
"steps": 1
} ]
}
```
I assumed that there is a small typo in "commments", so I used the name "commands" in the project. Due to this, copy-pasting this example input into the request body will cause an error.