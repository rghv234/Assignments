select license, salary 
from employee
group by license, salary

select avg(salary) as average_salary from employee

select sum(salary) as total_salary from employee

select sum(salary) from employee
where name like 'v%'
group by license, salary
having salary <= 10000