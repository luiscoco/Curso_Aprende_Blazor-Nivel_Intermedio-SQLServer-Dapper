#  How to integrate Dapper with a Blazor Web App and SQL Server

## 1. Run a Docker Container with SQL Server

Install and run Docker Desktop

Open a command prompt window and run this command:

```
docker run ^  -e "ACCEPT_EULA=Y" ^  -e "MSSQL_SA_PASSWORD=Luiscoco123456" ^  -p 1433:1433 ^  -d mcr.microsoft.com/mssql/server:2022-latest
```

![image](https://github.com/user-attachments/assets/8fdf1a18-5dd9-4ef7-b127-0f8b198c866a)

## 2. Connect to SQL Server Management Studio (SSMS)

Run SSMS and input the connection data

Password is: Luiscoco123456

![image](https://github.com/user-attachments/assets/e240dd9f-3697-429b-a744-aa5378cb82bd)
