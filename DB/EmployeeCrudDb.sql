/*
  Employee CRUD database setup
  Run this script in SQL Server Management Studio (SSMS).
  It creates EmployeeCrudDb and dbo.Employees.
*/
IF DB_ID(N'EmployeeCrudDb') IS NULL
BEGIN
    CREATE DATABASE EmployeeCrudDb;
END
GO

USE EmployeeCrudDb;
GO

IF OBJECT_ID(N'dbo.Employees', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Employees
    (
        Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Employees PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        Email NVARCHAR(150) NOT NULL,
        Department NVARCHAR(80) NOT NULL CONSTRAINT DF_Employees_Department DEFAULT N'',
        Salary DECIMAL(18,2) NOT NULL
    );
END
GO

-- Optional sample data (only inserts if the table is empty)
IF NOT EXISTS (SELECT 1 FROM dbo.Employees)
BEGIN
    INSERT INTO dbo.Employees (Name, Email, Department, Salary)
    VALUES
      (N'Ananya Rao', N'ananya.rao@example.com', N'Engineering', 850000),
      (N'Rahul Sharma', N'rahul.sharma@example.com', N'HR', 650000),
      (N'Sneha Kumar', N'sneha.kumar@example.com', N'Finance', 720000);
END
GO

SELECT Id, Name, Email, Department, Salary
FROM dbo.Employees
ORDER BY Id;
GO
