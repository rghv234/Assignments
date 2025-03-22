select pname, price from (select * from prod) as t

select avg(price) from prod
select pname, price from prod where price >= (select avg(price) from prod)

select t.pname, t.price from (select * from prod) as t

select pname, price from prod where price in(select avg(price) from prod group by pname)

select avg(price) as cost, pname from prod group by pname

select avg(price) as cost from prod group by pname
select pname, price from prod where price >= all(select avg(price) from prod group by pname)
select * from prod

select avg(price) from prod group by pname

select p1.pname, p1.price from prod as p1
join
prod as p
on p.pid = p1.pid
where p.price >= all(select avg(price) from prod group by pname)

select * from prod

select pname, price from prod where price <= any(select avg(price) from prod group by pname)
select pname, price from prod where price <= all(select avg(price) from prod group by pname)

select * from prod
select * from porder

select p.pid, p.pname,
(select count(productid) from porder where productid = p.pid group by productid) as total_order
from prod as p

select p.pid, p.pname, count(o.productid) as total_order
from prod as p
inner join
porder as o
on p.pid = o.productid
group by p.pname, p.pid

select pid, pname, price, (select orderdate from porder where productid = p.pid) from prod as p

