CREATE TRIGGER TR_TBLeMPLOYEE_delete
ON Employee
FOR DELETE
AS
BEGIN
DECLARE @Id INT
SELECT @Id=id from deleted

INSERT INTO tblEmployeeAudit VALUES (' Employee with the Id= '+cast(@Id as varchar(5)) +'Deleted at '
+cast(getdate() as varchar(20)))
END

select * from tblEmployee
select * from tblEmployeeAudit

delete from tblEmployee where id=4