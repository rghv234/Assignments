use marchdb

create table employee(
id int not null,
name varchar(max),
mobilenumber bigint,
email varchar(max),
dob date,
license varchar(50),
passport varchar(50)
)


alter table trainee add email varchar(50) null

alter table trainee alter column name varchar(max) null

alter table trainee drop column email

alter table employee add constraint pk_id primary key(id)

create table employeesalary(
salaryid int primary key not null
)