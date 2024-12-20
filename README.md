<h1 align="center" style="font-weight: bold;">
Sales Management Api💻</h1>

<p align="center">
 <a href="#tech">Technologies</a> • 
 <a href="#started">Getting Started</a> • 
 <a href="#routes">API Endpoints</a> •
 <a href="#tables">Tables</a>
</p>

<p align="center">
    <b>The Sales Management API provides endpoints for performing CRUD operations, calculating data,
    importing batch files, and generating reports based on the registered data.
    </b>
</p>

<h2 id="technologies">💻 Technologies/Tools/Patterns</h2>

- .Net Core 8
- SQL Server 
- Unit Test with XUnit
- Dapper
- JWT Authentication
- Dependency Injection
- Clean Artitetura
- Padrão repository
- Middleware, Serilog
- Sonar Cloud
- Docker


<h2 id="started">🚀 Getting started</h2>

Follow the steps below to deploy this application locally

<h3>Prerequisites</h3>

To clone and run you will need:

- Visual Studio 2022 IDE or Visual Studio Code
- Sql Server 18

<h3>Cloning</h3>

To clone the repository, simply execute the command below 
```bash
git clone sales-management-api
```

<h3>Starting</h3>

In this project, you can run dotnet run and navigate to http://localhost:5284.

```bash
dotnet run
```

<h2 id="routes">📍API Endpoints</h2>


| Route               | Description                                          
|----------------------|-----------------------------------------------------
| <kbd>POST /Authentication</kbd>     | authenticate user for API access 
| <kbd>GET /GetAllClients</kbd>     | List all registered clients
| <kbd>GET /GetByIdClient/{id}</kbd>     | List filtered client
| <kbd>POST /CreateClient</kbd>     | Create client registration 
| <kbd>PUT /UpdateClient/{id}</kbd>     | Update client registration 
| <kbd>DELETE /DeleteClient/{id}</kbd>     | Delete client registration 
| <kbd>GET /GetRelClients</kbd>     | fazer 
| <kbd>GET /GetAllCosts</kbd>     | List all registered costs
| <kbd>GET /GetByIdCost/{id}</kbd>     | List filtered cost
| <kbd>POST /CreateCost</kbd>     | Create cost registration 
| <kbd>PUT /UpdateCost/{id}</kbd>     | Update cost registration 
| <kbd>DELETE /DeleteCost/{id}</kbd>     | Delete cost registration 
| <kbd>GET /GetRelCostPrice</kbd>     | fazer
| <kbd>GET /GetAllProducts</kbd>     | List all registered products
| <kbd>GET /GetByIdProduct/{id}</kbd>     | List filtered product
| <kbd>POST /CreateProduct</kbd>     | Create product registration 
| <kbd>PUT /UpdateProduct/{id}</kbd>     | Update product registration 
| <kbd>DELETE /DeleteProduct/{id}</kbd>     | Delete product registration
| <kbd>POST /CreateProductCost</kbd>     | Create product cost registration 
| <kbd>GET /GetProductCostById/{id}</kbd>     | List filtered product cost
| <kbd>GET /GetAllProductCost</kbd>     | List all registered products costs
| <kbd>PUT /UpdateProductCost/{id}</kbd>     | Update product cost registration 
| <kbd>GET /GetAllUserProfile</kbd>     | List all registered user profile
| <kbd>GET /GetByUsernameProfile/{username}</kbd>     | List filtered profile
| <kbd>GET /UpdateUserProfile/{username}</kbd>     | Update profile registration  
| <kbd>GET /GetAllSales</kbd>     | List all registered sales
| <kbd>GET /GetByIdSale/{id}</kbd>     | List filtered sale
| <kbd>POST /CreateSale</kbd>     | Create sale registration 
| <kbd>PUT /UpdateSale/{id}</kbd>     | Update sale registration 
| <kbd>DELETE /DeleteSale/{id}</kbd>     | Delete sale registration
| <kbd>GET /GetRelQuantity</kbd>     | fazer
| <kbd>POST /UploadExcel</kbd>     | import excel spreadsheet with sales and costs in batch
| <kbd>POST /CreateUser</kbd>     | Create user registration 
| <kbd>POST /GetZipCode/{zipcode}</kbd>     | Search zip code from external api


<h3>POST /CreateUser</h3>

To access the endpoints, you first need to create a user and authenticate.

**REQUEST**
```json
{
  "username": "cinthia2024",
  "email": "cinthia2024@example.com",
  "name": "Cinthia Barbosa",
  "password": "12345@EwdT!",
  "confirmPassword": "12345@EwdT!"
}
```
**RESPONSE**
```json
{
  "isSuccess": true,
  "code": 16,
  "message": "Data created successfully!",
  "data": [
    true
  ]
}
```
<h3>POST /Authentication</h3>

**REQUEST**
```json
{
  "username": "cinthia2024",
  "email": "cinthia2024@example.com",
  "password": "12345@EwdT!"
}
```

**RESPONSE**
```json
{
  "isSuccess": true,
  "code": 200,
  "message": null,
  "data": [
    {
      "username": "cinthia2024",
      "email": "cinthia2024@example.com",
      "name": "Cinthia Barbosa",
      "active": true,
      "lastLogin": "2024-12-19T21:17:03.0785363-03:00",
      "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3MzQ2NTc0MjIsImlzcyI6IlVzZXJzIiwiYXVkIjoiZnVsbCJ9.e6RLEw_T3RKq4-LFKTBuUWkfGoPEQf0JxbqXDxWOccI",
      "tokenExpiration": "2024-12-19T22:17:03.078427-03:00"
    }
  ]
}
```

<h2 id="tables">📊 Tables</h2>

To run, you will need to configure your database connection in the <b>appsettings.json</b> file in the API. After that, run the database script in the Infrastructure/Script/base.sql folder.

![Tables](src/Infrastructure/Script/tables.png)
