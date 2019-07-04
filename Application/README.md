# MvcTestApp

#### Pre-Requisits:

1. [Download]((https://dotnet.microsoft.com/download)) and install NetCore SDK or Runtime 2.2.105 or newer.
2. After install open a CommandLine and run "dotnet --version" to verify the installation.
3. Run "dotnet dev-certs https --trust".

In case you want to use Visual Studio, these are supported versions:
```
VisualStudioVersion = 15.0.28307.421
MinimumVisualStudioVersion = 10.0.40219.1
```
#### Steps to run the app:
CommandLine:
1. Open a CommandLine and go to the path of MvcTestApp.csproj. Windows example: "cd c:\MvcTestApp\MvcTestApp".
2. run command **"dotnet run"**. It will build the solution and start the application at http://localhost:5000 or https://localhost:5001.
3. In the command line, move to the .sln folder, in the example "c:\MvcTestApp\" and type **"dotnet test"** to run all the test from solution.

VisualStudio
1. Open the MvcTestApp.sln with Visual Studio.
2. In the solution explorer, right click on MvcTestApp project and select **"Set as Startup project"**
3. Press F5 to run the solution.
4. Your predefined browser will open the login view.
5. Go to **Test -> run -> All Tests** to run all the tests from solution.

#### Built in users

| Id | UserName | Password | Roles |
| --- | --- | --- | --- |
| 00000000-0000-0000-0000-000000000001 | Admin | Admin | Admin, Page_1, Page_2 Page_3 |
| 00000000-0000-0000-0000-000000000002 | User1 | User1 | Page_1 |
| 00000000-0000-0000-0000-000000000003 | User2 | User2 | Page_2 |
| 00000000-0000-0000-0000-000000000004 | User3 | User3 | Page_3 |

#### Mvc Views URLs

These view will redirect to the login page in case of not authenticated user. After login, the user will be redirect to the page that was intended to access.

+ **Login view:** https://localhost:5001/login/login
+ **Page1 view:** https://localhost:5001/page1
+ **Page2 view:** https://localhost:5001/page2
+ **Page3 view:** https://localhost:5001/page3

#### Api URLs

[Basic authentication](https://developer.mozilla.org/es/docs/Web/HTTP/Headers/Authorization) header is required.
Suported mediatypes are **application/json** and **application/xml**.

Endpoints available for all users:

+ [GET] https://localhost:5001/api/users/{id}
+ [GET] https://localhost:5001/api/users

Endpoints available for Admin user:
+ [POST] https://localhost:5001/api/user
+ PUT] https://localhost:5001/api/users/{id}
+ [DELETE] https://localhost:5001/api/users/{id}

Enjoy!
