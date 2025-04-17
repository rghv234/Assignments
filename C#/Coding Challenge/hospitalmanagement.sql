use hospitalmanagement

create table Patients (
patientId int primary key identity(1,1),
firstName varchar(50) not null,
lastName varchar(50) not null,
dateOfBirth date not null,
gender varchar(50) not null,
contactNumber varchar(15) not null,
address varchar(200) not null
);

create table Doctors (
doctorId int primary key identity(1,1),
firstName varchar(50) not null,
lastName varchar(50) not null,
specialization varchar(50) not null,
contactNumber varchar(15) not null
);

create table Appointments (
appointmentId int primary key identity(1,1),
patientId int not null,
doctorId int not null,
appointmentDate date not null,
description varchar(500),
foreign key (patientId) references Patients(patientId) on delete cascade,
foreign key (doctorId) references Doctors(doctorId) on delete cascade
);