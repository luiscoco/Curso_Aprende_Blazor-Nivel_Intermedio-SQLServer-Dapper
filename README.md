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

![image](https://github.com/user-attachments/assets/97fe1efd-c3c0-45b8-87e2-b51605d00912)

Leave all the default values and press the Create button

![image](https://github.com/user-attachments/assets/d580a161-0bef-4154-a4fe-3b724ed19f3f)

## 7. Add the Nuget packages

Install the required NuGet packages:

