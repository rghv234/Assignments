select avg(price) from prod group by pname
select pname, price from prod where price >All(select avg(price) from prod group by pname)
select * from prod

-- join query to find out average price of product and search minimum price
select p1.pname, p1.price from prod as p1
join
prod as p
on p.pid =p1.pid