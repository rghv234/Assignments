create database payxpert

use payxpert

create table employee
(
employeeid int identity(1,1) primary key not null,
firstname varchar(255),
lastname varchar(255),
dateofbirth date not null,
gender varchar(50),
email varchar(255),
phonenumber varchar(20) not null,
address text not null,
position varchar(255),
joiningdate date not null,
terminationdate date
)

create table payroll
(
payrollid int identity(1,1) primary key not null,
employeeid int,
constraint fk_payroll_employee foreign key (employeeid) references employee(employeeid),
payperiodstartdate date not null,
payperiodenddate date not null,
basicsalary int not null,
overtimepay int not null,
deductions int not null,
netsalary int not null
)

create table tax
(
taxid int identity(1,1) primary key not null,
employeeid int,
constraint fk_tax_employee foreign key (employeeid) references employee(employeeid),
taxyear int not null,
taxableincome int not null,
taxamount int not null
)

create table financialrecord
(
recordid int identity(1,1) primary key not null,
employeeid int,
constraint fk_financialrecord_employee foreign key (employeeid) references employee(employeeid),
recorddate date not null,
description varchar(255),
amount int not null,
recordtype varchar(255)
)
