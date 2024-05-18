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
DA DECIMAL(10, 2),
RA DECIMAL(10, 2),
OvertimeHours INT,
OvertimePay DECIMAL(10,2),
Deductions DECIMAL(10,2),
GrossSalary DECIMAL(10,2),
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

-- Insert Values into the table
INSERT INTO Employee (FirstName, LastName, DateOfBirth, Gender, Email, PhoneNumber, [Address], Position, JoiningDate, TerminationDate) VALUES
('John', 'Doe', '1980-05-15', 'Male', 'john.doe@example.com', '1234567890', '123 Main St', 'Manager', '2010-01-01', NULL),
('Jane', 'Smith', '1985-07-20', 'Female', 'jane.smith@example.com', '0987654321', '456 Elm St', 'Developer', '2012-03-15', NULL),
('Robert', 'Brown', '1975-12-30', 'Male', 'robert.brown@example.com', '1231231234', '789 Oak St', 'Analyst', '2014-07-01', NULL),
('Alice', 'Johnson', '1990-11-01', 'Female', 'alice.johnson@example.com', '1236547890', '101 Maple St', 'Designer', '2015-05-20', NULL),
('Charlie', 'Williams', '1982-04-10', 'Male', 'charlie.williams@example.com', '9876543210', '202 Pine St', 'HR', '2013-06-15', NULL),
('Emily', 'Davis', '1992-08-25', 'Female', 'emily.davis@example.com', '4567890123', '303 Cedar St', 'Developer', '2016-11-30', NULL),
('Daniel', 'Miller', '1987-03-14', 'Male', 'daniel.miller@example.com', '3214567890', '404 Birch St', 'Manager', '2011-08-25', NULL),
('Sophie', 'Taylor', '1984-06-18', 'Female', 'sophie.taylor@example.com', '7890123456', '505 Spruce St', 'Analyst', '2017-09-10', NULL),
('Thomas', 'Moore', '1986-09-29', 'Male', 'thomas.moore@example.com', '6543210987', '606 Willow St', 'Designer', '2014-03-05', NULL),
('Emma', 'Anderson', '1993-01-19', 'Female', 'emma.anderson@example.com', '4561237890', '707 Fir St', 'HR', '2018-07-23', NULL);

INSERT INTO Payroll (EmployeeID, PayPeriodStartDate, PayPeriodEndDate, BasicSalary, DA, RA, OvertimeHours, OvertimePay, Deductions, GrossSalary, NetSalary) VALUES
(1, '2023-05-01', '2023-05-31', 30000, 5000, 2000, 10, 13333.33, 2000, 43333.33, 41333.33),
(2, '2023-05-01', '2023-05-31', 25000, 4000, 1500, 8, 10000.00, 2000, 35000.00, 33000.00),
(3, '2023-05-01', '2023-05-31', 28000, 4500, 1800, 12, 14400.00, 2000, 42400.00, 40400.00),
(4, '2023-05-01', '2023-05-31', 32000, 5200, 2100, 5, 8666.67, 2000, 40666.67, 38666.67),
(5, '2023-05-01', '2023-05-31', 22000, 3500, 1400, 7, 7466.67, 2000, 29466.67, 27466.67),
(6, '2023-05-01', '2023-05-31', 29000, 4700, 1900, 9, 12400.00, 2000, 41400.00, 39400.00),
(7, '2023-05-01', '2023-05-31', 31000, 5000, 2000, 6, 9600.00, 2000, 40600.00, 38600.00),
(8, '2023-05-01', '2023-05-31', 27000, 4300, 1700, 11, 12533.33, 2000, 39533.33, 37533.33),
(9, '2023-05-01', '2023-05-31', 24000, 3800, 1600, 8, 9600.00, 2000, 33600.00, 31600.00),
(10, '2023-05-01', '2023-05-31', 26000, 4200, 1500, 10, 11066.67, 2000, 37066.67, 35066.67);

INSERT INTO Tax (EmployeeID, TaxYear, TaxableIncome, TaxAmount) VALUES
(1, 2023, 496000.00, 64700.00),
(2, 2023, 396000.00, 48700.00),
(3, 2023, 484800.00, 61720.00),
(4, 2023, 464000.04, 59100.01),
(5, 2023, 329600.04, 33940.01),
(6, 2023, 472800.00, 59920.00),
(7, 2023, 463200.00, 58480.00),
(8, 2023, 450399.96, 56559.99),
(9, 2023, 379200.00, 44880.00),
(10, 2023, 420800.04, 52120.01);

INSERT INTO FinancialRecord (EmployeeID, RecordDate, [Description], Amount, RecordType) VALUES
(1, '2023-05-31', 'Salary Payment', 41333.33, 'Credit'),
(2, '2023-05-31', 'Salary Payment', 33000.00, 'Credit'),
(3, '2023-05-31', 'Salary Payment', 40400.00, 'Credit'),
(4, '2023-05-31', 'Salary Payment', 38666.67, 'Credit'),
(5, '2023-05-31', 'Salary Payment', 27466.67, 'Credit'),
(6, '2023-05-31', 'Salary Payment', 39400.00, 'Credit'),
(7, '2023-05-31', 'Salary Payment', 38600.00, 'Credit'),
(8, '2023-05-31', 'Salary Payment', 37533.33, 'Credit'),
(9, '2023-05-31', 'Salary Payment', 31600.00, 'Credit'),
(10, '2023-05-31', 'Salary Payment', 35066.67, 'Credit');

CREATE TABLE Users (
Username VARCHAR(50),
password VARCHAR(50))

INSERT INTO Users (Username, Password) VALUES 
('user1', 'password1'),
('user2', 'password2'),
('user3', 'password3'),
('user4', 'password4'),
('user5', 'password5'),
('user6', 'password6'),
('user7', 'password7'),
('user8', 'password8'),
('user9', 'password9'),
('user10', 'password10');