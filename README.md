## Contact organizer Angular 7 test case

This small application is just a prototype of Angular 7 + ASP.NET Core 2.2 + Entity Framework Core + SQL Server stack. It basically manages a list of contacts.

### Development environment setup

To setup the development environment you need to have:

- Visual Studio 2017 or newer
- .NET Core 2.2 and ASP.NET Core 2.2 SDK
- SQL Server database
- Node.js and npm
- Angular 7

1. Create your own copy of SQL Server database. Application is looking for database listed under "ContactOrganizerDb" in appsettings.json. If your database name is different, update the connection string as needed. 
2. Run script ContactOrganizer.Data.SqlServer/Scripts/DbCreate.sql against your database.
3. Start the application in Visual Studio 2017. I will not be covering deployment options (docker, IIS etc) in this small tutorial.

### Design notes

This example uses hexagonal architecture (a.k.a. Ports and adapters). You can read more about that [here](https://blog.ndepend.com/hexagonal-architecture/). Main entry to the domain layer API is `ContactOrganizerService` facade class that is being injected with `IContactOrganizerRepository` repository implementation - in our case a SQL server implementation using Entity Framework Core. Domain layer is responsible for validation of parameters and it throws `FluentValidation.ValidationException` in case of errors. This design decision has been made to simplify creation of other potential client applications.

Presentation has been made using ASP.NET Core Web API + Angular 7. 