-- create customers table
create table customers (
    customerid int primary key,
    name varchar(100),
    email varchar(100),
    balance decimal(10,2)
)

-- create products table
create table products (
    productid int primary key,
    name varchar(100),
    price decimal(10,2),
    stockquantity int
)

-- create orders table
create table orders (
    orderid int primary key,
    customerid int,
    orderdate datetime,
    totalamount decimal(10,2),
    status varchar(20)
)

-- first transaction: buying 1 product
begin transaction

if exists (select 1 from products where productid = 1 and stockquantity >= 1)
begin
    update products 
    set stockquantity = stockquantity - 1 
    where productid = 1

    update customers 
    set balance = balance - 299 
    where customerid = 1

    insert into orders (orderid, customerid, orderdate, totalamount, status)
    values (101, 1, getdate(), 299, 'completed')

    commit transaction
end
else
begin
    rollback transaction
    print 'not enough stock to complete the order'
end

-- second transaction: buying 5 products
begin transaction

declare @totalcost decimal(10,2)
set @totalcost = 299 * 5

if exists (select 1 from customers where customerid = 3 and balance >= @totalcost)
begin
    update products 
    set stockquantity = stockquantity - 5 
    where productid = 1

    update customers 
    set balance = balance - @totalcost 
    where customerid = 3

    commit transaction
end
else
begin
    rollback transaction
    raiserror ('insufficient balance', 16, 1)
end

-- view final results
select * from customers
select * from products
select * from orders

------------------------------------------------------------------

begin transaction

select quantity into @current_quantity
from stocks
where product_id = 101 and store_id = 1
for update

if @current_quantity >= 2
begin
update stocks
set quantity = quantity - 2
where product_id = 101 and store_id = 1

commit transaction
end
else
begin
rollback transaction
end
-----------------------------------------------------------------
begin transaction

declare @bike_stock int
declare @productid int = 1
declare @orderid int = 201
declare @price decimal(10,2)
declare @quantity int = 1

select @bike_stock = stockquantity, @price = price
from products
where productid = @productid

if @bike_stock >= quantity
update products
set stockquantity = stockquantity - @quantity
where productid = @productid

insert into orders (orderid, customerid, orderdate, totalamount, status)
values (@orderid, @customerid, getdate(), @price * @quantity, 'completed')

insert into order_items (orderid, productid, quantity, price)
values (@orderid, @productid, @quantity, @price)

commit transaction
end
else
begin
rollback transaction
raiserror ('bike is out of stock', 16, 1)
end