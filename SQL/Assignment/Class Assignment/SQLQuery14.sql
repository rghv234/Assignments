CREATE FUNCTION dbo. CalculateBonus(@salary decimal)
Returns Decimal(10,2)
as
begin
return @salary*0.10
end

