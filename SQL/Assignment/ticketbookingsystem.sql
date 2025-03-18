create database ticketbookingsystem

use ticketbookingsystem

create table venue (
venue_id int identity(1,1) primary key,
venue_name varchar(255) not null,
address text not null
)

create table event (
event_id int identity(1,1) primary key,
event_name varchar(255) not null,
event_date date not null,
event_time time not null,
venue_id int,
total_seats int check (total_seats > 0),
available_seats int check (available_seats >= 0),
ticket_price decimal(10,2) check (ticket_price >= 0),
event_type varchar(50) check (event_type in ('movie', 'sports', 'concert')),
constraint fk_event_venue foreign key (venue_id) references venue(venue_id)
)

create table customer (
customer_id int identity(1,1) primary key,
customer_name varchar(255) not null,
email varchar(255) unique not null,
phone_number varchar(15) unique not null
)

create table booking (
booking_id int identity(1,1) primary key,
customer_id int not null,
event_id int not null,
num_tickets int check (num_tickets > 0),
total_cost decimal(10,2),
booking_date datetime default getdate(),
constraint fk_booking_customer foreign key (customer_id) references customer(customer_id),
constraint fk_booking_event foreign key (event_id) references event(event_id)
)

insert into venue (venue_name, address) values
('Grand Theater', '123 Main St, City A'),
('Rock Stadium', '456 Broadway, City B'),
('Cineplex 10', '789 Oak St, City C'),
('Open Air Arena', '1011 Palm Rd, City D'),
('Royal Hall', '1213 King Ave, City E'),
('City Concert Hall', '1415 Queen St, City F'),
('Mega Sports Complex', '1617 Champion Blvd, City G'),
('Sunshine Amphitheater', '1819 Beachside Ave, City H'),
('Cultural Auditorium', '2021 Heritage Lane, City I'),
('Skyview Dome', '2223 Skyline Rd, City J')

insert into event (event_name, event_date, event_time, venue_id, total_seats, available_seats, ticket_price, event_type) values
('Avengers Premiere', '2025-04-10', '18:00:00', 3, 200, 150, 500, 'Movie'),
('Coldplay Live', '2025-06-20', '20:00:00', 6, 5000, 4500, 3000, 'Concert'),
('World Cup Final', '2025-07-15', '19:30:00', 7, 80000, 70000, 5000, 'Sports'),
('The Phantom Play', '2025-05-12', '17:00:00', 5, 300, 250, 1200, 'Movie'),
('NBA Finals', '2025-06-10', '19:00:00', 7, 25000, 20000, 4500, 'Sports'),
('Rock Fest', '2025-08-05', '21:00:00', 2, 10000, 9000, 2500, 'Concert'),
('Sunset Jazz Night', '2025-09-18', '19:00:00', 8, 500, 480, 1800, 'Concert'),
('Horror Night', '2025-10-31', '22:00:00', 3, 200, 190, 700, 'Movie'),
('Opera Extravaganza', '2025-11-25', '18:30:00', 5, 400, 380, 2500, 'Concert'),
('Championship Boxing', '2025-12-12', '20:30:00', 7, 15000, 14000, 4000, 'Sports')

insert into customer (customer_name, email, phone_number) values
('John Doe', 'john.doe@email.com', '9876543201'),
('Jane Smith', 'jane.smith@email.com', '8765432102'),
('Alice Johnson', 'alice.j@email.com', '7654321093'),
('Bob Williams', 'bob.w@email.com', '6543210984'),
('Charlie Brown', 'charlie.b@email.com', '5432109875'),
('David Miller', 'david.m@email.com', '4321098766'),
('Emma Wilson', 'emma.w@email.com', '3210987657'),
('Frank Moore', 'frank.m@email.com', '2109876548'),
('Grace Davis', 'grace.d@email.com', '1098765439'),
('Hank Adams', 'hank.a@email.com', '9988776600')

insert into booking (customer_id, event_id, num_tickets, total_cost) values
(1, 2, 3, 9000),
(2, 3, 2, 10000),
(3, 5, 1, 4500),
(4, 6, 4, 10000),
(5, 1, 5, 2500),
(6, 9, 2, 5000),
(7, 7, 6, 10800),
(8, 4, 3, 3600),
(9, 8, 2, 1400),
(10, 10, 8, 32000)

select * 
from event

select * 
from event 
where available_seats > 0

select * 
from event 
where event_name like '%cup%'

select * 
from event 
where ticket_price between 1000 and 2500

select * 
from event 
where event_date between '2025-06-01' and '2025-09-01'

select * 
from event 
where available_seats > 0 and event_type = 'concert'

select * 
from customer 
order by customer_id 
offset 5 rows fetch next 5 rows only

select * 
from booking 
where num_tickets > 4

select * 
from customer 
where phone_number like '%000'

select * 
from event 
where total_seats > 15000 
order by total_seats desc

select * from event 
where event_name not like 'x%' 
and event_name not like 'y%' 
and event_name not like 'z%'

select event_type, avg(ticket_price) as avg_ticket_price
from event
group by event_type

select event_id, sum(total_cost) as total_revenue
from booking
group by event_id

select top 1 event_id, sum(num_tickets) as total_tickets_sold
from booking
group by event_id
order by total_tickets_sold desc

select event_id, sum(num_tickets) as total_tickets_sold
from booking
group by event_id

select * 
from event
where event_id not in (select distinct event_id from booking)

select top 1 customer_id, sum(num_tickets) as total_tickets
from booking
group by customer_id
order by total_tickets desc

select
format(event_date, 'yyyy-mm') as event_month, 
event.event_id,
sum(num_tickets) as total_tickets_sold
from event
join booking on event.event_id = booking.event_id
group by format(event_date, 'yyyy-mm'), event.event_id
order by event_month

select venue_id, avg(ticket_price) as avg_ticket_price
from event
group by venue_id

select event_type, sum(num_tickets) as total_tickets_sold
from event
join booking on event.event_id = booking.event_id
group by event_type

select year(event_date) as event_year, sum(total_cost) as total_revenue
from event
join booking on event.event_id = booking.event_id
group by year(event_date)

select customer_id, count(distinct event_id) as total_events
from booking
group by customer_id
having count(distinct event_id) > 1

select customer_id, sum(total_cost) as total_spent
from booking
group by customer_id

select event_type, venue_id, avg(ticket_price) as avg_ticket_price
from event
group by event_type, venue_id

select customer_id, sum(num_tickets) as total_tickets_purchased
from booking
where booking_date >= dateadd(day, -30, getdate())
group by customer_id

select venue_id,
(select avg(ticket_price)
from event e2
where e1.venue_id = e2.venue_id) as avg_ticket_price
from event e1
group by venue_id

select event_id
from booking
group by event_id
having sum(num_tickets) >
(select 0.5 * sum(total_seats)
from event e
where e.event_id = booking.event_id)

select customer_id
from customer c
where not exists (
select 1 from booking b where c.customer_id = b.customer_id)

select event_id, event_name
from event
where event_id not in (select distinct event_id from booking)

select event_type, sum(total_tickets_sold) as total_tickets
from (
select e.event_type, sum(b.num_tickets) as total_tickets_sold
from event e
join booking b on e.event_id = b.event_id
group by e.event_type
) as ticket_summary
group by event_type

select event_id, event_name, ticket_price
from event
where ticket_price > (select avg(ticket_price) from event)

select customer_id,
(select sum(total_cost)
from booking b
where b.customer_id = c.customer_id) as total_revenue
from customer c

select customer_id
from booking
where event_id in (
select event_id from event where venue_id = 'some_venue_id'
)

select event_type,
(select sum(num_tickets)
from booking b
where b.event_id = event_id) as total_tickets_sold
from event e
group by event_type

select customer_id, format(booking_date, '%y-%m') as booking_month
from booking
where customer_id in (
select distinct customer_id from booking
)
group by customer_id, format(booking_date, 'yyyy-mm')

select venue_id,
(select avg(ticket_price)
from event e2
where e1.venue_id = e2.venue_id) as avg_ticket_price
from event e1
group by venue_id


