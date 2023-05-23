-- Step 1: Database and Schema Creation
Go
IF db_id('LabExam_SaraAhmed_PetDB') IS NULL 
    CREATE DATABASE LabExam_SaraAhmed_PetDB
Go
use LabExam_SaraAhmed_PetDB
Go
create table [Owner](
OwnerID int primary key,
Fname varchar(50),
Lname varchar(50),
email varchar(50)
)
create table Vet(
VetId int primary key,
Fname varchar(50),
Lname varchar(50),
Address varchar(100)
)
-- Note: because Pet depends on FKs from Owner and Vet, it must be created last.
create table Pet(
PetID int primary key,
[Name] varchar(50),
BirthYear int,
OwnerID int foreign key references [Owner](OwnerID),
VetID int foreign key references Vet(VetID)
)

-- Step 2: Value Insertion
go
insert into Vet values (21, 'Ahmed', 'Hamed', 'Cairo')
insert into Vet values (22, 'Mostafa', 'Emad', 'Giza')
insert into Vet values (23, 'Leila', 'Hany', 'Cairo')

insert into [Owner] values (11, 'Samy', 'Mahmoud',  'samy.mahmoud@gmail.com')
insert into [Owner] values (12, 'Nour', 'Elsayed',  'nour.elsayed@gmail.com')
insert into [Owner] values (13, 'Sondos', 'Hesham',  'sondos.hesham@gmail.com')

insert into Pet values (1, 'Fluffy', '2022', 11, 21)
insert into Pet values (2, 'Wag', '2020', 13, 21)
insert into Pet values (3, 'Fluffy', '2021', 13, 22)
insert into Pet values (4, 'Tweet', '2022', 12, 23)
go

select PetID as PetID, [Name] as PetName, (2023 - BirthYear) as [age], ([Owner].Fname + ' ' + [Owner].Lname) as OwnerName, (Vet.Fname + ' ' + Vet.Lname) as VetName 
from Pet left join Vet on Pet.VetID = Vet.VetId left join [Owner] on Pet.OwnerID = [Owner].OwnerID

-- Note: I created a stored procedure to help me in the web app when using the Add New Pet form
-- You can alternatively use a raw query directly from the web app
go
create procedure addNewPet
@id int, @name varchar(50), @year int
as 
insert into Pet values (@id, @name, @year, null, null)
go

-- testing the proc:
exec addNewPet @id = 5, @name = 'dodie', @year = 4

select * from Pet

delete from Pet where PetID=5 or PetID=6 or PetID=7