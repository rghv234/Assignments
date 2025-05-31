CREATE PROC Usp_GetEmployeeById
(@empid as int)
AS
BEGIN
SELECT ID, NAME, GENDER, LOCATION FROM EMPLOYEE WHERE ID=@empid
END

exec Usp_GetEmployeeById 102

