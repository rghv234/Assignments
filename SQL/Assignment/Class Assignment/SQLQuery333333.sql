-- Example for Stored procedure without param
CREATE PROCEDURE usp_GetAllEmployees
AS
BEGIN
SELECT * FROM Employee
END
EXECUTE usp_GetAllEmployees
EXEC usp_GetAllEmployees
usp_GetAllEmployees