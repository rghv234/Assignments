CREATE PROC Usp_AddNewEmployee(
@name varchar(20),@gender varchar(10),@location varchar(50)
)
AS
BEGIN
INSERT INTO EMPLOYEE (NAME, GENDER, LOCATION) VALUES (@NAME,@GENDER,@LOCATION)
END

EXEC Usp_AddNewEmployee @gender='Male' ,@name='Allen',@location='bangalore'

select * from Employee