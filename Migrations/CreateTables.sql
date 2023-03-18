CREATE TABLE Comment (
	Id int not null identity(1,1) primary key,
	Message nvarchar(250) not null,
	Timestamp datetime2 not null
)

CREATE TABLE Status (
	Id int not null identity(1,1) primary key,
	StatusCode int not null unique
)

INSERT INTO Status VALUES(0)
INSERT INTO Status VALUES(1)
INSERT INTO Status VALUES(2)
GO

CREATE TABLE Customer (
	Id int not null identity(1,1) primary key,
	FirstName nvarchar(50) not null,
	LastName nvarchar(50) not null, 
	PhoneNumber varchar(15) null,
	Email varchar(150) not null unique,
)
GO

CREATE TABLE Ticket (
	Id int not null identity(1,1) primary key,
	Code uniqueidentifier not null unique,
	Description nvarchar(max) not null,
	Timestamp datetime2 not null, 
	CustomerId int not null references Customer(Id),
	StatusId int not null references Status(Id)
)
GO

CREATE TABLE Comments (
	Id int not null identity(1,1) primary key,
	TicketId int not null references Ticket(Id),
	CommentId int not null references Comment(Id)
)
GO