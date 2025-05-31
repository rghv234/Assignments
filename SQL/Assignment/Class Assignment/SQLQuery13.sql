create proc Usp_NumberOfEmployeesByLocation(
@location varchar(50)
)
as
begin
select count(*) as number from employee where location=@location
end
go

exec Usp_NumberOfEmployeesByLocation 'chennai'