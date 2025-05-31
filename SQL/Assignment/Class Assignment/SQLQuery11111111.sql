
use Hexa_May_25

create table Employee
(
id int identity(101,1),
name varchar(20),
gender varchar(10),
location varchar(50),
doj date,
salary money

)

insert into Employee
values ('Hariharan', 'Male', 'chennai' , '1-6-2025' , 35000),
('Alhan', 'Male' ,'Hyderabad','6-6-2025' , 36000)
, ('Arun','Male','Bangalore','10-6-2025' , 37000)
, ('Geethica','Female','chennai','1-6-2025',38000)
, ('Nithya sri', 'Female' , 'Hyderabad','10-6-2025' , 39000)

