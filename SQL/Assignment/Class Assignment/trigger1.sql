CREATE TRIGGER trg_PreventDropEmployee
ON DATABASE
FOR DROP_TABLE
AS
BEGIN
DECLARE @eventData XML = EVENTDATA();
IF @eventData.value('(/EVENT_INSTANCE/ObjectName) [1]', 'NVARCHAR(128)') = 'Employee'
BEGIN
PRINT 'Dropping the Employee table is not allowed!';
ROLLBACK;

END

END;

drop table Employee