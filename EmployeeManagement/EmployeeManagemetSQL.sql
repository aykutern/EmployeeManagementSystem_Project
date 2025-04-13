create database EmployeeManagement;
use EmployeeManagement;
create table Users(
UserID INT Primary Key Identity(1,1),
Username VARCHAR(100) Not null Unique,
Password VARCHAR(255) Not null ,
Role VARCHAR(50) Default 'User'
);

create table Employees(
EmployeeID INT Primary Key Identity(1,1),
Name VARCHAR(255) Not null,
Position VARCHAR(255) Not null,
Salary FLOAT Not null
);

insert into Users values('admin', 'admin','Admin'),('aykut', 'test', 'User');

select * from Users;

insert into Employees values('berk', 'lawyer',20000.0);

select * from Employees;

