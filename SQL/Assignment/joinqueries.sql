create table skill (
    id int primary key,
    skill_name varchar(50)
)

create table train (
    trainee_id int primary key,
    skill_id int,
    trainee_name varchar(50),
    foreign key (skill_id) references skill(id)
)

insert into skill (id, skill_name) values
(1, 'python'),
(2, 'java'),
(3, 'sql'),
(4, 'c++')

insert into train (trainee_id, skill_id, trainee_name) values
(101, 1, 'alice'),
(102, 2, 'bob'),
(103, 3, 'charlie'),
(104, 1, 'david'),
(105, null, 'eve')

select * from skill
select * from train

select * 
from skill
inner join train
on skill.id = train.skill_id

select * 
from skill
left join train
on skill.id = train.skill_id

select * 
from skill
right join train
on skill.id = train.skill_id

select * 
from skill
full outer join train
on skill.id = train.skill_id
