CREATE TRIGGER tr_tblEmployee_insert
ON Employee
FOR INSERT
AS
BEGIN
DECLARE @Id INT
SELECT @Id=id from INSERTED

INSERT INTO tblEmployeeAudit VALUES ('New Employee with Id= '+CAST(@id as varchar(5))+' is added at '+
cast(Getdate() as nvarchar(20)))
END

select * from Employee
select * from tblEmployeeAudit
insert into tblEmployee values (4, 'santo',4554, 'Male', 2)