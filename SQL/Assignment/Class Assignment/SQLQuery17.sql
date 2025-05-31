-- Multi-statement Table-Valued Function
CREATE FUNCTION dbo.HighSalaryEmployee (@avgsalary DECIMAL)
RETURNS @HighEarners TABLE (
    id INT,
    name VARCHAR(20),
    salary DECIMAL(10,2)
)
AS
BEGIN
    INSERT INTO @HighEarners
    SELECT id, name, salary
    FROM Employee
    WHERE salary > @avgsalary;

    RETURN;
END;
GO

-- Call the function
SELECT * FROM dbo.HighSalaryEmployee(36000);
