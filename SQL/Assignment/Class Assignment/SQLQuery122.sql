declare @empCount int
exec Usp_NumberOfEmployeesByLocation 'chennai', @empCount output
print @empCount