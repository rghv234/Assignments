-- Inline Table-Valued Function
CREATE FUNCTION dbo.GetEmployeeDetailsByLocation (@location VARCHAR(50))
RETURNS TABLE
AS
-- Semicolon before RETURN is required in many cases
RETURN
(
    SELECT id, name, gender, location
    FROM Employee
    WHERE location = @location
);
GO

-- Call the function
SELECT *
FROM dbo.GetEmployeeDetailsByLocation('Bangalore');
