CREATE TABLE Customers (
CustomerID INT PRIMARY KEY,
Name VARCHAR(100),
Email VARCHAR(100),
Balance DECIMAL(10,2)

);

CREATE TABLE Products (
ProductID INT PRIMARY KEY,
Name VARCHAR(100),
Price DECIMAL(10,2),
StockQuantity INT

);

CREATE TABLE Orders (
OrderID INT PRIMARY KEY,
CustomerID INT,
OrderDate DATETIME,
TotalAmount DECIMAL(10,2),
Status VARCHAR(20)
);

BEGIN TRANSACTION;

IF (EXISTS(SELECT 1 FROM Products WHERE ProductID=1 AND StockQuantity>=1))
BEGIN
-- UPDATE THE PRODUCT STOCK
UPDATE Products SET StockQuantity=StockQuantity-1

-- UPDATE CUSTOMERS BALANCE
UPDATE Customers SET Balance=Balance-299
WHERE CustomerID=1

insert into Orders (orderId, CustomerID, OrderDate, TotalAmount, Status) values
(101,1,GETDATE(),299,'Completed')

select * from Customers

COMMIT TRANSACTION

END

select * 
from Customers

select * from Products

Select * from orders

-- rollBack Transaction
BEGIN TRAN
DECLARE @Totalcost decimal(10,2)=299*5

if(Exists(select 1 from Customers where CustomerID = 3 and Balance > @TotalCost))
begin

update products set StockQuantity=StockQuantity-5
where ProductID = 1

update customers set Balance=Balance-@Totalcost
where CustomerID=3
end
else
begin 
rollback
raiserror('Insufficient Balance',16,1)
end