-- Create Payroll Management System Database
CREATE DATABASE PayXpert
-- Use the Payroll system
USE PayXpert

-- Create Database Schema for PayXpert
-- Create tables in SQL Schema with appropriate class and write the unit test case for the application.
CREATE TABLE Employee(
EmployeeID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
FirstName VARCHAR(20) NOT NULL,
LastName VARCHAR(20) ,
DateOfBirth DATE NOT NULL,
Gender VARCHAR(20) NOT NULL,
Email VARCHAR(50) NOT NULL,
PhoneNumber VARCHAR(10) NOT NULL,
[Address] VARCHAR(250) NOT NULL,
Position VARCHAR(30) NOT NULL,
JoiningDate DATE NOT NULL,
TerminationDate DATE) 

CREATE TABLE Payroll(
PayrollID INT IDENTITY(101,1) NOT NULL PRIMARY KEY,
EmployeeID INT,
FOREIGN KEY(EmployeeID) REFERENCES Employee(EmployeeID),
PayPeriodStartDate DATE,
PayPeriodEndDate DATE,
BasicSalary DECIMAL(10,2),
OvertimePay DECIMAL(10,2),
Deductions DECIMAL(10,2),
NetSalary DECIMAL(10,2))

CREATE TABLE Tax(
TaxID INT IDENTITY(1001,1) NOT NULL PRIMARY KEY,
EmployeeID INT,
FOREIGN KEY(EmployeeID) REFERENCES Employee(EmployeeID),
TaxYear INT,
TaxableIncome DECIMAL(10,2),
TaxAmount DECIMAL(10,2))

CREATE TABLE FinancialRecord(
RecordID INT IDENTITY(10001,1) NOT NULL PRIMARY KEY,
EmployeeID INT,
FOREIGN KEY(EmployeeID) REFERENCES Employee(EmployeeID),
RecordDate DATE,
[Description] VARCHAR(250),
Amount DECIMAL(10,2),
RecordType VARCHAR(50))

-- Insert Values into the Database Schema
-- Inserting values into the Employee table with Indian data
INSERT INTO Employee (FirstName, LastName, DateOfBirth, Gender, Email, PhoneNumber, [Address], Position, JoiningDate)
VALUES
    ('Rahul', 'Gupta', '1988-09-25', 'Male', 'rahulgupta@example.com', '9123456789', '123 MG Road, Delhi, India', 'Senior Developer', '2022-01-15'),
    ('Priya', 'Sharma', '1990-06-12', 'Female', 'priyasharma@example.com', '9988776655', '456 Park Street, Mumbai, India', 'HR Manager', '2021-03-20'),
    ('Amit', 'Patel', '1995-03-08', 'Male', 'amitpatel@example.com', '9876543210', '789 Lakeview Avenue, Bangalore, India', 'Software Engineer', '2023-02-10');

-- Inserting values into the Payroll table with Indian data
INSERT INTO Payroll (EmployeeID, PayPeriodStartDate, PayPeriodEndDate, BasicSalary, OvertimePay, Deductions, NetSalary)
VALUES
    (1, '2024-04-01', '2024-04-15', 55000, 2500, 7500, 52500),
    (2, '2024-04-01', '2024-04-15', 60000, 3000, 8000, 55000),
    (3, '2024-04-01', '2024-04-15', 45000, 2000, 5000, 43000);

-- Inserting values into the Tax table with Indian data
INSERT INTO Tax (EmployeeID, TaxYear, TaxableIncome, TaxAmount)
VALUES
    (1, 2024, 660000, 132000),
    (2, 2024, 720000, 144000),
    (3, 2024, 540000, 108000);

-- Inserting values into the FinancialRecord table with Indian data
INSERT INTO FinancialRecord (EmployeeID, RecordDate, [Description], Amount, RecordType)
VALUES
    (1, '2024-04-10', 'Bonus', 20000, 'Income'),
    (2, '2024-04-12', 'Travel Expense', 10000, 'Expense'),
    (3, '2024-04-14', 'Training Cost', 15000, 'Expense');
