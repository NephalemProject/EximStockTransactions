# EximStockTransactions README

This document will contain the instructions on how to run the software solution.

## Pre-requisites

Before running the application, ensure you have the following installed:

- .NET SDK 9.0 or higher
- Visual Studio 2022 / VS Code
- Dotnet-EF tool:

```dotnet tool install --global dotnet-ef```

If necessary, run the migrations for the DB from the API project folder:

```dotnet ef database update```


## Running the Application

### Running the Application via Visual Studio

Change your startup item to "Web and API". This is configured to run both the front-end and API project for ease of use.

### Run the API via Command Line

Within the EximStockTransactions.Api project, run the following:

`dotnet run`

By default, the API runs on https://localhost:7000.

### Run the front-end via Command Line
Within the EximStockTransactions.Web project, run the following:

`dotnet run`

The frontend will run on https://localhost:3000 and call the API.


### Running Unit Tests

Unit tests were implemented using xUnit and Moq for ItemService.
They can be run within Visual Studio, otherwise you can run them using the command line by navigating to the EximStockTransactions.Tests project and running:

```dotnet test```