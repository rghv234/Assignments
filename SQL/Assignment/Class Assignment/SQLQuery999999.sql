declare @salary money
EXEC Usp_GetEmployeeSalaryById @id = 103, @salary = @salary OUT
select @salary

DECLARE @ename varchar(40) -- variable declaration
-- set @ename='Peter' -- assigning the valu for variable
select @ename=name from employee where id=101
print @ename