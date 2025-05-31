--- stored procedure with in, out paramater
CREATE PROC Usp_GetEmployeeSalaryById(@ID INT,
@salary money OUTPUT)
AS
BEGIN
select @salary=salary from employee where id=@id
END

