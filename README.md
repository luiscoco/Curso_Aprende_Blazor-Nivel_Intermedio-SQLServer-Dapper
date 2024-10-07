#  How to integrate Dapper with a Blazor Web App and SQL Server

## 1. Run a Docker Container with SQL Server

Install and run **Docker Desktop**

Open a command prompt window and run this command:

```
docker run ^  -e "ACCEPT_EULA=Y" ^  -e "MSSQL_SA_PASSWORD=Luiscoco123456" ^  -p 1433:1433 ^  -d mcr.microsoft.com/mssql/server:2022-latest
```

![image](https://github.com/user-attachments/assets/8fdf1a18-5dd9-4ef7-b127-0f8b198c866a)

## 2. Connect to SQL Server Management Studio (SSMS)

Run **SSMS** and input the connection data

Password is: Luiscoco123456

![image](https://github.com/user-attachments/assets/e240dd9f-3697-429b-a744-aa5378cb82bd)

## 3. Create the database 

Run this command to create a new database

```sql
CREATE DATABASE BlazorAppDB;
GO
```

and

```sql
USE BlazorAppDB;
GO
```

## 4. Create the database table

For creating the table in the database run this command:

```sql
-- Create the table for products
CREATE TABLE Products (
    ProductId INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(100) NOT NULL,
    Price DECIMAL(18, 2) NOT NULL,
    Quantity INT NOT NULL
);
```

## 5. Seed the database with data

Populate the database with some data

```sql
-- Seed the table with sample data
INSERT INTO Products (ProductName, Price, Quantity)
VALUES ('Laptop', 1000.00, 10),
       ('Smartphone', 500.00, 50),
       ('Tablet', 300.00, 30),
       ('Headphones', 50.00, 100),
       ('Monitor', 200.00, 20);
```

## 6. Create a Blazor Web Application in Visual Studio 2022 Community Edition

Run Visual Studio and create a new project

![image](https://github.com/user-attachments/assets/000ec328-1122-444b-b5cb-c0316d84b5a8)

Search for Blazor project templates and select Blazor Web App and press the next button

![image](https://github.com/user-attachments/assets/f7411d00-90a4-4e57-b3a0-0add4144d5fb)

Input the project name and chose the project location in the hard disk and press the next button 



Leave all the default values and press the Create button



## 7. Add the Nuget packages

Install the required NuGet packages:

Dapper

Microsoft.Data.SqlClient

System.Data.Common

![image](https://github.com/user-attachments/assets/7f187b26-ad18-4e6a-9b52-0e9b79e1798d)

## 8. Create a new "Data" folder for including the repository and data model

![image](https://github.com/user-attachments/assets/e3fe5f98-a0c5-452d-898a-937563424b75)

**IDbConnectionFactory.cs**

```csharp
using System.Data;

namespace BlazorWebAppWithDapper.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
```

**Product.cs**

```csharp
namespace BlazorWebAppWithDapper.Data
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

}
```

**ProductsRepository.cs**

```csharp
using System.Data;
using Dapper;

namespace BlazorWebAppWithDapper.Data
{
    public class ProductsRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public ProductsRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        // Get all products
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryAsync<Product>("GetAllProducts", commandType: CommandType.StoredProcedure);
        }

        // Get a single product by Id
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Product>(
                "GetProductById",
                new { ProductId = productId },
                commandType: CommandType.StoredProcedure);
        }

        // Insert a new product
        public async Task<int> InsertProductAsync(Product product)
        {
            using var connection = _connectionFactory.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("ProductName", product.ProductName);
            parameters.Add("Price", product.Price);
            parameters.Add("Quantity", product.Quantity);

            return await connection.ExecuteAsync("InsertProduct", parameters, commandType: CommandType.StoredProcedure);
        }

        // Update an existing product
        public async Task<int> UpdateProductAsync(Product product)
        {
            using var connection = _connectionFactory.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("ProductId", product.ProductId);
            parameters.Add("ProductName", product.ProductName);
            parameters.Add("Price", product.Price);
            parameters.Add("Quantity", product.Quantity);

            return await connection.ExecuteAsync("UpdateProduct", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
```

**SqlDbConnectionFactory.cs**

```csharp
using Microsoft.Data.SqlClient;
using System.Data;

namespace BlazorWebAppWithDapper.Data
{
    public class SqlDbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public SqlDbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }

}
```


