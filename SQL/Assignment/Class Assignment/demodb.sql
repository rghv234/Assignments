CREATE DATABASE DemoDB;
GO

USE DemoDB;
GO

CREATE TABLE Students (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Age INT,
    Email NVARCHAR(100)
);
GO

INSERT INTO Students (Name, Age, Email) VALUES 
('Ravi Kumar', 20, 'ravi@example.com'),
('Anita Sharma', 22, 'anita@example.com');
