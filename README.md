# Ecommerce WebAPI

Built with .NET Core 7 and PostgreSQL. Link to deployment [here](https://ecommerce-13.azurewebsites.net/index.html)

## Features

Minimum Web API project built as an ECommerce Platform with 6 main entities:

1. User
2. Product
3. Order
4. OrderItem
5. Review
6. Category

## Authentication

The authentication is built from scratch with email and password instead of using IdentityManager class.

## Authorization

Ths project makes use of Policy-based and resource-based authorization

## Installation

Make sure you have `dotnet` and `dotnet ef` CLI in your local machine

1. Clone the project to your local machine
2. Create file `appSettings.json`, see example from `example.appSettings.json`
3. Restore all the dependencies `dotnet restore`
4. Create a local PostgreSQL database, according to your connection string in `appSettings.json`
5. Run migration `dotnet ef migrations add your-migration-name`
6. Update database `dotnet ef database update`
7. Run project with `dotnet run` or watch mode `dotnet watch`
