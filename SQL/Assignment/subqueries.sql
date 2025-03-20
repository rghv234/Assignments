select pname, price from (select * from prod) as t
select avg(price) from prod
select pname, price from prod where price >= (select avg(price) from prod)

select avg(price), pname from prod group by pname, price
select pname,price from prod where price <= any(select avg(price) from prod group by pname, price)

select price from (select * from prod) as t
select pname,price from prod where price <= all(select avg(price) from prod group by pname, price)

select p.pid, p.pname, (select count(productid) from porder where productid = p.pid) as total_order from prod as p