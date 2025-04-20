DELETE FROM employee;
DBCC CHECKIDENT ('employee', RESEED, 0);
