use marchdb

insert into employee (id, name, mobilenumber, email, dob, license, passport)
values(102, 'vijay',59687436,'abc@gmail.com', '1999-12-12','lic156','psp789')

select 
e.id as employee_ID, 
e.name as employee_name, 
e.mobilenumber as employee_mobilenumber, 
e.email as employee_emailID, 
e.dob as employee_dob,
e.license as employee_license,
e.passport as employee_passport 
from employee e
