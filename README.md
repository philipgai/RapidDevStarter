# Getting Started
## Requirements
- Visual Studio 2019
  - Workloads
    - ASP.NET and web development
    - Data storage and processing
    - .NET Core 3.1
- Visual Studio Code
- SQL Server connection
- npm 6.14.4 & node v12.17.0

## Setup
1. Clone repo
2. Open in Visual Studio 2019
3. Build the solution
4. Deploy the database by double-clicking publishing profile "RapidDevStarter.Database.Local.publish.xml"
    - (Recommended) Change SQL Server login password
5. Set RapidDevStarter.Api as Startup Project
6. Click play and the API will start and launch Swagger automatically
7. Open RapidDevStarter.Web > rapid-dev-starter in Visual Studio Code
8. In the terminal in Visual Studio Code, run `npm i`
9. Once complete, run `npm start` (which will run `ng serve --open`)
10. Your browser should open localhost:4200. Navigate to the Users page and test adding, updating, and deleting users using the DevExtreme grid which uses the OData queries and API endpionts.

# Technology Stack
## Angular Web App
- Angular 10.0.5
- Bootstrap 4.5.0
- Font Awesome 5.13.0
- DevExtreme 20.1-stable
- RxJS 6.5.5

## ASP.NET Core OData Web API with Swagger
- ASP.NET Core 3.1.3
- NET Core 3.1
- AutoMapper 10.0.0
- Swashbuckle.AspNetCore 5.5.1
- Microsoft.AspNetCore.OData
- Entity Framework Core 3.1.6

## Microsoft SQL Server Database Project
- One-click dacpac deployment
- Database completely managed in source control