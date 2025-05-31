create proc Usp_NumberOfEmployeesByLocation(
@location varchar(50),
@number int output
)
as
begin
select @number=count(*) from employee where location=@location
end
go

