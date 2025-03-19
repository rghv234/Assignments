create table colors
(
id int identity,
color varchar(50)
)

create table shape
(
id int identity,
name varchar(50)
)

insert into shape values('circle'),('square'),('rectangle'),('triangle')
insert into colors values('red'),('green'),('blue')

select * from shape
select * from colors

select s.name, c.color from shape as s
cross join
colors as c

