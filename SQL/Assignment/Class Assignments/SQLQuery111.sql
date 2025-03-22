create table prod
(
pname varchar(50),
pid int primary key not null,
price int not null
)
insert into prod (pname, pid,price) values
('notebook',200, 2000),
('laptop',201, 3000),
('pen',202, 4000),
('toys', 203, 5000)

select * from prod

create table porder
(
orid int identity(200,1) primary key not null,
orderdate date,
productid int,
constraint fk_productid foreign key(productid) references prod(pid)
)

insert into porder values(GETDATE(), (select pid from prod where pname = 'pen')),
(GETDATE(), (select pid from prod where pname = 'notebook')),
(GETDATE(), (select pid from prod where pname = 'laptop')),
(GETDATE(), (select pid from prod where pname = 'pen'))

update porder set productid=(select pid from prod where pname='pen')
where orid=202

select * from prod
select * from porder

delete from porder where productid=203
delete from prod where pid not in (select productid from porder)
select * from prod
select * from porder