# Program Placement Console Application

## Overview

This console application, built using .NET 7.0, serves as an implementation of CRUD APIs for four tabs: Program, Application Template, Workflow, and Preview.
The application leverages Azure Cosmos DB for NoSQL data storage and incorporates dependency injection for clean, maintainable code. Additionally, it includes unit tests using xUnit.

## Getting Started

### Clone the Repository

```shell
git clone https://github.com/Kimonic99/CapitalApi.git
```
## Navigate to the Project Directory

## Install Dependencies
```shell
dotnet restore
```
## Configure Azure Cosmos DB
Set up your Azure Cosmos DB Emulator for local testing and configure appsettings.json with your Cosmos DB connection settings 
(CosmosSettings:AccountUri, CosmosSettings:AccountKey, and CosmosSettings:DatabaseName).

## Build and Run
```shell
dotnet run
```
Access the application through your web browser or Swagger.

## Project Structure
### Models:

ProgramModel.cs
Template.cs
Workflow.cs
Preview.cs
These represent the data structures for the four tabs.

### DTOs: 
The DTO folder contains Data Transfer Object (DTO) classes responsible for data transfer between APIs and clients.

### Profiles: 
AutoMapper profiles reside in the Profiles folder, facilitating mapping between model and DTO classes.

### Repositories: 
The Repositories folder houses repository classes, responsible for handling CRUD operations for each tab.

### Program.cs: 
This is the main entry point for the console application. It configures services, sets up the database context, and defines API endpoints.

### Tests: 
The Tests folder includes unit tests for models and APIs using xUnit.

## API Endpoints

### Program Tab

POST /api/programs: Create a new program.
GET /api/programs: Retrieve a list of programs.
GET /api/programs/{id}: Retrieve a program by ID.
PUT /api/programs/{id}: Update a program by ID.

### Application Template Tab

GET /api/templates: Retrieve a list of application templates.
GET /api/templates/{id}: Retrieve an application template by ID.
PUT /api/templates/{id}: Update an application template by ID.

### Workflow Tab

GET /api/workflows: Retrieve a list of workflows with stages.
PUT /api/workflows/{id}/{newStage}: Update the stage of a workflow by ID.

### Preview Tab

GET /api/preview: Retrieve a summary of all program details.

### Unit Testing
To run unit tests, use the following command:

```shell
dotnet test
```
### Contributing
Contributions to this project are welcome. Please submit issues or pull requests to help improve the application.

### License
This project is licensed under the MIT License. For details, see the LICENSE file.
