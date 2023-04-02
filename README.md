# MoneyMe App

Migarate Database in your Machine:

  1. Check if you have EntityFrameworkCore.SqlServer, Data.Sql, EntityFrameworkCore, EntityFrameworkCore.Design, EntityFrameworkCore.Tools
  2. In the MoneyMeApp API Open Nuget Package Manager Console run Update-Database
  3. You should be able to see the MoneyMeV2 DB in your local database or SQL Server Object Explorer

How To Run the Project
  1. Run First the MoneyMeApp API, Copy the local host present in the Swagger
  2. Update the BaseURL in the MoneyMeWebApplication > Controllers > HomeController.cs file with the localhost in the swagger e.g."https://localhost/5000/" then save
  3. Click the project solution > properties > Common Properties
  4. Under StartUp Project, select Multiple Startup Project
  5. MoneyMeApp should be on the top then select Start on the dropdown list for both MoneyMeApp and MoneyMeWebApplication
  6. Click Apply then OK
  7. Build > Start

How to add the initial customer using MoneyMeApp API and load the existing data using the app

  1. You can add via Swagger using hte /api/Customer POST request or use the app and click calculate Qoute
  2. you can load the data by passing the customer Id in a query parameter in the url e.g."http://localhost:18868/?id=1"

  
 
