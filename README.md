# HeadlessCms

Headless CMS was developed using following stack of technologies:
  - .NET Core 3.1
  - ASP.NET Core Web API
  - Docker (for containerization of Web API app and MS SQL database)
  - MS SQL database

Additionals nuget packages:
  - Automapper (for mapping entities, resources and dtos)
  - FluentValidation (for model validation)
  - Mediatr (CQRS pattern)
  - Swashbuckle (For describing RESTfull operations)
  - EntityyFrameworkCore
  - FluentAssertions (Test project)
  - TestHost (for integration testing)
  - InMemory (extension for ef db context)
  - NUnit
  - etc

# How to run application
Preconditions:
  - Install docker and switch to linux containers
  - Install Visual studio (preferable 2019)

Steps: 
  1. Download or clone [HeadlessCms Project](https://github.com/Haletski/HeadlessCms.git)
  2. Navigate to HeadlessCms/Articles.WebAPI folder open any comand line terminal and run "docker compose up". It might take up to 10 minutes to download and build application and sql images.
  3. When you see such logs in the terminal than you will be able to open and use application.

  ![image](https://user-images.githubusercontent.com/30402551/135578148-4aca3432-57ad-48f0-8878-0471c9c9af4f.png)

  4. Web application should be available at localhost:8000.

  ![image](https://user-images.githubusercontent.com/30402551/135578243-5bdf03ec-152f-45aa-965b-24ca2b65e6d0.png)

  5. POST,PUT and DELETE operations are secured with API key as per requirement. Click "Authorize" button and paste following API key "b53cae95-ac71-44c9-bbf1-9311427f4c10" if you want to test these endpoints too.
  6. All operations, validation rules, status codes and etc are fully documented in swagger document.
  > Attention!!! First call of any endpoint might take a bit longer time ~ 10 seconds, because entity framework should create database and migrate schema.

# How to run integration tests
Preconditions:
  - Install Visual studio (preferable 2019)

Steps:
  1. Download or clone [HeadlessCms Project](https://github.com/Haletski/HeadlessCms.git)
  2. Open solution of HeadlessCMS in Visual Studio
  3. Press right click on Articles.WebAPI.Integration.Tests project and choose "Run Tests" from context menu. All tests should be successfully passed.
  ![image](https://user-images.githubusercontent.com/30402551/135579251-875d188e-836e-420b-9715-cb1839c91486.png)
