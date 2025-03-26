-- Ensure the database is created and selected
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'petpals')
    CREATE DATABASE petpals
GO

USE petpals
GO

-- Create tables
CREATE TABLE shelters
(
    ShelterID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    Name VARCHAR(255),
    Location VARCHAR(255)
)

CREATE TABLE pets
(
    PetID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    Name VARCHAR(255),
    Age INT NOT NULL,
    Breed VARCHAR(255),
    Type VARCHAR(255),
    AvailableForAdoption BIT NOT NULL,
    ShelterID INT,
    OwnerID INT,
    CONSTRAINT fk_pets_shelters FOREIGN KEY (ShelterID) REFERENCES shelters(ShelterID)
)

CREATE TABLE Donations
(
    DonationID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    DonorName VARCHAR(255),
    DonationType VARCHAR(255),
    DonationAmount DECIMAL,
    DonationItem VARCHAR(255),
    DonationDate DATETIME,
    ShelterID INT,
    CONSTRAINT fk_donations_shelters FOREIGN KEY (ShelterID) REFERENCES shelters(ShelterID)
)

CREATE TABLE AdoptionEvents
(
    EventID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    EventName VARCHAR(255),
    EventDate DATETIME,
    Location VARCHAR(255)
)

CREATE TABLE Participants
(
    EventID INT,
    ParticipantID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    ParticipantName VARCHAR(255),
    ParticipantType VARCHAR(255),
    CONSTRAINT fk_participants_adoptionevents FOREIGN KEY (EventID) REFERENCES AdoptionEvents(EventID)
)

-- Insert data
INSERT INTO shelters (Name, Location) 
VALUES 
    ('Happy Pets Shelter', 'Mumbai'), 
    ('Animal Heaven', 'Chennai')

INSERT INTO pets (Name, Age, Breed, Type, AvailableForAdoption, ShelterID) 
VALUES
    ('John', 2, 'Golden Retriever', 'Dog', 1, 1),
    ('Ginger', 3, 'Siamese', 'Cat', 1, 2),
    ('Charlie', 4, 'Pomeranian', 'Dog', 0, 1)

INSERT INTO Donations (DonorName, DonationType, DonationAmount, DonationItem, DonationDate, ShelterID) 
VALUES
    ('Nisha', 'Monetary', 100.00, NULL, '2024-03-01', 1),
    ('Kriti', 'Item', NULL, 'Dog Food', '2024-03-05', 2)

INSERT INTO AdoptionEvents (EventName, EventDate, Location) 
VALUES
    ('Spring Adoption Fair', '2024-04-10', 'Mumbai'),
    ('Summer Pet Meet', '2024-06-15', 'Chennai')

INSERT INTO Participants (ParticipantName, ParticipantType, EventID) 
VALUES
    ('Rishi', 'Adopter', 1),
    ('Vijay', 'Volunteer', 2)

-- Update shelters
UPDATE shelters
SET Name = 'Rescue Pets', Location = 'Delhi'
WHERE ShelterID = 1

-- Run queries
SELECT Name, Age, Breed, Type 
FROM pets 
WHERE AvailableForAdoption = 1

SELECT ParticipantName, ParticipantType 
FROM Participants 
WHERE EventID = 1

SELECT COALESCE(SUM(Donations.DonationAmount), 0) AS total_donation
FROM shelters
LEFT JOIN Donations ON shelters.ShelterID = Donations.ShelterID
GROUP BY shelters.Name

SELECT Name, Age, Breed, Type 
FROM pets 
WHERE OwnerID IS NULL

SELECT FORMAT(DonationDate, 'MMMM yyyy') AS monthyear, SUM(DonationAmount) AS total_donation
FROM Donations
GROUP BY FORMAT(DonationDate, 'MMMM yyyy')

SELECT DISTINCT Breed 
FROM pets 
WHERE Age BETWEEN 1 AND 3 AND Age > 5  

SELECT shelters.Name, COUNT(pets.PetID) AS pets_available
FROM shelters
JOIN pets ON shelters.ShelterID = pets.ShelterID
GROUP BY shelters.Name

SELECT COUNT(*) AS total_participants
FROM Participants
JOIN AdoptionEvents ON Participants.EventID = AdoptionEvents.EventID
WHERE AdoptionEvents.Location = 'Chennai'

SELECT DISTINCT Breed 
FROM pets 
WHERE Age BETWEEN 1 AND 5

SELECT * 
FROM pets 
WHERE AvailableForAdoption = 1

SELECT shelters.Name, COUNT(pets.PetID) AS available_pets
FROM shelters
JOIN pets ON shelters.ShelterID = pets.ShelterID
WHERE pets.AvailableForAdoption = 1
GROUP BY shelters.Name

SELECT p1.Name AS pet1, p2.Name AS pet2, p1.Breed, s.Name AS shelter_name
FROM pets p1
JOIN pets p2 ON p1.Breed = p2.Breed AND p1.PetID < p2.PetID
JOIN shelters s ON p1.ShelterID = s.ShelterID

SELECT shelters.Name AS shelter_name, AdoptionEvents.EventName 
FROM shelters
CROSS JOIN AdoptionEvents

SELECT shelters.Name, COUNT(pets.PetID) AS adopted_pets
FROM shelters
JOIN pets ON shelters.ShelterID = pets.ShelterID
GROUP BY shelters.Name
ORDER BY adopted_pets DESC
