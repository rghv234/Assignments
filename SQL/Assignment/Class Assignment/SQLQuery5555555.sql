CREATE PROC usp_GetEmployeebyGenderLocation
(@gender as varchar(10),
@location as varchar(50)
)
AS
BEGIN
SELECT * FROM Employee WHERE gender=@gender AND LOCATION=@location
END

EXEC usp_GetEmployeebyGenderLocation 'Male','Chennai'