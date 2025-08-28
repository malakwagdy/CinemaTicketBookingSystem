-- Create tables
CREATE TABLE Users(
    Email NVARCHAR(50) PRIMARY KEY,
    UserPassword NVARCHAR(20),
    UserType BIT,
    PhoneNumber NVARCHAR(15),
    FirstName NVARCHAR(20),
    LastName NVARCHAR(20),
    BirthDate DATE
);

CREATE TABLE Movie(
    MovieID INT PRIMARY KEY IDENTITY(1,1),
    Director NVARCHAR(50),
    Title NVARCHAR(200),
    Genre NVARCHAR(20),
    AgeRating INT,
    ReleaseDate DATE,
    AdminID NVARCHAR(50),
    FOREIGN KEY (AdminID) REFERENCES Users(Email)
);

CREATE TABLE Cinema(
    CinemaID INT PRIMARY KEY IDENTITY(1,1),
    CinemaName NVARCHAR(50),
    CLocation NVARCHAR(200),
    AdminID NVARCHAR(50),
    FOREIGN KEY (AdminID) REFERENCES Users(Email)
);

CREATE TABLE Hall(
    HallID INT PRIMARY KEY IDENTITY(1,1),
    HallName NVARCHAR(10),
    ScreenType NCHAR(2),
    CinemaID INT,
    AdminID NVARCHAR(50),
    FOREIGN KEY (AdminID) REFERENCES Users(Email),
    FOREIGN KEY (CinemaID) REFERENCES Cinema(CinemaID)
);

CREATE TABLE Seat(
    SeatNumber INT,
    RowNumber NCHAR(1),
    SeatType NVARCHAR(10),
    HallID INT,
    AdminID NVARCHAR(50),
    PRIMARY KEY (SeatNumber, RowNumber, HallID),
    FOREIGN KEY (AdminID) REFERENCES Users(Email),
    FOREIGN KEY (HallID) REFERENCES Hall(HallID)
);

CREATE TABLE MoviesActors(
    MovieID INT,
    Actor NVARCHAR(100),
    PRIMARY KEY(MovieID, Actor),
    FOREIGN KEY (MovieID) REFERENCES Movie(MovieID)
);

CREATE TABLE Booking(
    BookingID INT PRIMARY KEY IDENTITY(1,1),
    TotalPrice FLOAT,
    BookingDate DATETIME,
    CustomerID NVARCHAR(50),
    FOREIGN KEY (CustomerID) REFERENCES Users(Email)
);

CREATE TABLE Showtimes(
    StartTime DATETIME,
    AdminID NVARCHAR(50),
    HallID INT,
    MovieID INT,
    PRIMARY KEY (StartTime, HallID, MovieID),
    FOREIGN KEY (HallID) REFERENCES Hall(HallID),
    FOREIGN KEY (MovieID) REFERENCES Movie(MovieID),
    FOREIGN KEY (AdminID) REFERENCES Users(Email)
);

CREATE TABLE Ticket(
    TicketID INT IDENTITY(1,1),
    BookingID INT,
    StartTime DATETIME,
    SeatNumber INT,
    RowNumber NCHAR(1),
    MovieID INT,
    HallID INT,
    Price FLOAT,
    PRIMARY KEY (TicketID, BookingID, StartTime, SeatNumber, RowNumber, MovieID, HallID),
    FOREIGN KEY (BookingID) REFERENCES Booking(BookingID),
    FOREIGN KEY (StartTime, HallID, MovieID) REFERENCES Showtimes(StartTime, HallID, MovieID),
    FOREIGN KEY (SeatNumber, RowNumber, HallID) REFERENCES Seat(SeatNumber, RowNumber, HallID)
);

GO

-- Stored procedures
CREATE PROCEDURE CreateHallSeats
    @HallID INT,
    @NumRows INT,
    @SeatsPerRow INT,
    @IsPremiumHall BIT = 0,
    @PremiumRowsFromEnd INT = 3,
    @AdminID NVARCHAR(50) = 'admin@email.com'
AS
BEGIN
    SET NOCOUNT ON;

    IF @PremiumRowsFromEnd < 0 OR @PremiumRowsFromEnd > @NumRows
    BEGIN
        RAISERROR('Premium rows from end must be between 0 and the number of rows', 16, 1);
        RETURN;
    END

    DELETE FROM Seat WHERE HallID = @HallID;

    DECLARE @RowNum INT = 0;
    DECLARE @RowChar NCHAR(1);
    DECLARE @SeatNum INT;
    DECLARE @PremiumRowStartIndex INT = @NumRows - @PremiumRowsFromEnd;

    WHILE @RowNum < @NumRows
    BEGIN
        SET @RowChar = CHAR(65 + @RowNum);
        SET @SeatNum = 1;

        WHILE @SeatNum <= @SeatsPerRow
        BEGIN
            DECLARE @SeatType NVARCHAR(10);
            IF @IsPremiumHall = 1 OR @RowNum >= @PremiumRowStartIndex
                SET @SeatType = 'Premium';
            ELSE
                SET @SeatType = 'Standard';

            INSERT INTO Seat (SeatNumber, RowNumber, SeatType, HallID, AdminID)
            VALUES (@SeatNum, @RowChar, @SeatType, @HallID, @AdminID);

            SET @SeatNum = @SeatNum + 1;
        END

        SET @RowNum = @RowNum + 1;
    END
END;

GO

CREATE PROCEDURE CreateHallWithSeats
    @HallName NVARCHAR(10),
    @ScreenType NCHAR(2),
    @CinemaID INT,
    @AdminID NVARCHAR(50),
    @NumRows INT,
    @SeatsPerRow INT,
    @IsPremiumHall BIT = 0,
    @PremiumRowsFromEnd INT = 3
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID)
    VALUES (@HallName, @ScreenType, @CinemaID, @AdminID);

    DECLARE @NewHallID INT = SCOPE_IDENTITY();

    EXEC CreateHallSeats 
        @HallID = @NewHallID,
        @NumRows = @NumRows,
        @SeatsPerRow = @SeatsPerRow,
        @IsPremiumHall = @IsPremiumHall,
        @PremiumRowsFromEnd = @PremiumRowsFromEnd,
        @AdminID = @AdminID;
END;

GO

-- Trigger
CREATE TRIGGER trg_HallAfterInsert
ON Hall
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @HallID INT;
    SELECT @HallID = HallID FROM inserted;

    DECLARE @NumRows INT = 7;
    DECLARE @SeatsPerRow INT = 7;
    DECLARE @IsPremiumHall BIT = 0;
    DECLARE @PremiumRowsFromEnd INT = 3;
    DECLARE @AdminID NVARCHAR(50);
    SELECT @AdminID = AdminID FROM inserted;

    EXEC CreateHallSeats 
        @HallID = @HallID,
        @NumRows = @NumRows,
        @SeatsPerRow = @SeatsPerRow,
        @IsPremiumHall = @IsPremiumHall,
        @PremiumRowsFromEnd = @PremiumRowsFromEnd,
        @AdminID = @AdminID;
END;

GO

-- Data insertions
INSERT INTO Users (Email, UserPassword, UserType, PhoneNumber, FirstName, LastName, BirthDate) 
VALUES ('admin@email.com', 'admin1234', 0, '01562786425', 'Admin', 'Account', '1987-09-02');

INSERT INTO Users (Email, UserPassword, UserType, PhoneNumber, FirstName, LastName, BirthDate) 
VALUES ('amr@email.com', 'amr1234', 1, '01542793516', 'Amr', 'Emad', '2004-10-22');

INSERT INTO Users (Email, UserPassword, UserType, PhoneNumber, FirstName, LastName, BirthDate) 
VALUES ('sara@email.com', 'sara5678', 1, '01234567891', 'Sara', 'Hassan', '1999-05-14');

INSERT INTO Users (Email, UserPassword, UserType, PhoneNumber, FirstName, LastName, BirthDate) 
VALUES ('mohamed@email.com', 'moh1234', 1, '01122334455', 'Mohamed', 'Ali', '2001-08-30');

INSERT INTO Users (Email, UserPassword, UserType, PhoneNumber, FirstName, LastName, BirthDate) 
VALUES ('laila@email.com', 'lai9090', 1, '01099887766', 'Laila', 'Kamal', '2003-12-01');

INSERT INTO Users (Email, UserPassword, UserType, PhoneNumber, FirstName, LastName, BirthDate) 
VALUES ('youssef@email.com', 'you7890', 1, '01211223344', 'Youssef', 'Mostafa', '1998-04-22');

INSERT INTO Users (Email, UserPassword, UserType, PhoneNumber, FirstName, LastName, BirthDate) 
VALUES ('dina@email.com', 'din5566', 1, '01566778899', 'Dina', 'Magdy', '2000-09-17');

INSERT INTO Users (Email, UserPassword, UserType, PhoneNumber, FirstName, LastName, BirthDate) 
VALUES ('omar@email.com', 'oma1122', 1, '01012345678', 'Omar', 'Sami', '2002-03-05');

INSERT INTO Users (Email, UserPassword, UserType, PhoneNumber, FirstName, LastName, BirthDate) 
VALUES ('rana@email.com', 'ran3344', 1, '01199887766', 'Rana', 'Zaki', '2005-07-20');

INSERT INTO Users (Email, UserPassword, UserType, PhoneNumber, FirstName, LastName, BirthDate) 
VALUES ('ahmed@email.com', 'ahm4567', 1, '01298765432', 'Ahmed', 'Nabil', '1997-11-11');

INSERT INTO Users (Email, UserPassword, UserType, PhoneNumber, FirstName, LastName, BirthDate) 
VALUES ('salma@email.com', 'sal7788', 1, '01512345670', 'Salma', 'Fouad', '2006-06-09');

select * from users



INSERT INTO Movie (Director, Title, Genre, AgeRating, ReleaseDate, AdminID)
VALUES ('Christopher Nolan', 'Inception', 'Sci-Fi', 13, '2010-07-16', 'admin@email.com');

INSERT INTO Movie (Director, Title, Genre, AgeRating, ReleaseDate, AdminID)
VALUES ('Steven Spielberg', 'Jurassic Park', 'Adventure', 13, '1993-06-11', 'admin@email.com');

INSERT INTO Movie (Director, Title, Genre, AgeRating, ReleaseDate, AdminID)
VALUES ('James Cameron', 'Avatar', 'Sci-Fi', 13, '2009-12-18', 'admin@email.com');

INSERT INTO Movie (Director, Title, Genre, AgeRating, ReleaseDate, AdminID)
VALUES ('Peter Jackson', 'The Lord of the Rings: The Fellowship of the Ring', 'Fantasy', 12, '2001-12-19', 'admin@email.com');

INSERT INTO Movie (Director, Title, Genre, AgeRating, ReleaseDate, AdminID)
VALUES ('Quentin Tarantino', 'Pulp Fiction', 'Crime', 18, '1994-10-14', 'admin@email.com');

INSERT INTO Movie (Director, Title, Genre, AgeRating, ReleaseDate, AdminID)
VALUES ('Francis Ford Coppola', 'The Godfather', 'Crime', 17, '1972-03-24', 'admin@email.com');

INSERT INTO Movie (Director, Title, Genre, AgeRating, ReleaseDate, AdminID)
VALUES ('George Lucas', 'Star Wars: Episode IV - A New Hope', 'Sci-Fi', 10, '1977-05-25', 'admin@email.com');

INSERT INTO Movie (Director, Title, Genre, AgeRating, ReleaseDate, AdminID)
VALUES ('Ridley Scott', 'Gladiator', 'Action', 15, '2000-05-05', 'admin@email.com');

INSERT INTO Movie (Director, Title, Genre, AgeRating, ReleaseDate, AdminID)
VALUES ('Martin Scorsese', 'The Wolf of Wall Street', 'Drama', 18, '2013-12-25', 'admin@email.com');

INSERT INTO Movie (Director, Title, Genre, AgeRating, ReleaseDate, AdminID)
VALUES ('David Fincher', 'Fight Club', 'Drama', 18, '1999-10-15', 'admin@email.com');

INSERT INTO Movie (Director, Title, Genre, AgeRating, ReleaseDate, AdminID)
VALUES ('Robert Zemeckis', 'Forrest Gump', 'Drama', 13, '1994-07-06', 'admin@email.com');

INSERT INTO Movie (Director, Title, Genre, AgeRating, ReleaseDate, AdminID)
VALUES ('Christopher Nolan', 'The Dark Knight', 'Action', 13, '2008-07-18', 'admin@email.com');

INSERT INTO Movie (Director, Title, Genre, AgeRating, ReleaseDate, AdminID)
VALUES ('Denis Villeneuve', 'Dune', 'Sci-Fi', 13, '2021-10-22', 'admin@email.com');

INSERT INTO Movie (Director, Title, Genre, AgeRating, ReleaseDate, AdminID)
VALUES ('Greta Gerwig', 'Lady Bird', 'Drama', 15, '2017-11-03', 'admin@email.com');

INSERT INTO Movie (Director, Title, Genre, AgeRating, ReleaseDate, AdminID)
VALUES ('Bong Joon-ho', 'Parasite', 'Thriller', 16, '2019-05-30', 'admin@email.com');

INSERT INTO Movie (Director, Title, Genre, AgeRating, ReleaseDate, AdminID)
VALUES ('Alfonso Cuarón', 'Gravity', 'Sci-Fi', 13, '2013-10-04', 'admin@email.com');

INSERT INTO Movie (Director, Title, Genre, AgeRating, ReleaseDate, AdminID)
VALUES ('Taika Waititi', 'Jojo Rabbit', 'Comedy', 12, '2019-10-18', 'admin@email.com');

INSERT INTO Movie (Director, Title, Genre, AgeRating, ReleaseDate, AdminID)
VALUES ('Jordan Peele', 'Get Out', 'Horror', 16, '2017-02-24', 'admin@email.com');

INSERT INTO Movie (Director, Title, Genre, AgeRating, ReleaseDate, AdminID)
VALUES ('Stanley Kubrick', 'The Shining', 'Horror', 18, '1980-05-23', 'admin@email.com');

INSERT INTO Movie (Director, Title, Genre, AgeRating, ReleaseDate, AdminID)
VALUES ('Wes Anderson', 'The Grand Budapest Hotel', 'Comedy', 14, '2014-03-28', 'admin@email.com');


SELECT * FROM Movie

INSERT INTO Cinema (CinemaName, CLocation, AdminID) 
VALUES ('Galaxy Cinema', '6th October City, Giza', 'admin@email.com');

INSERT INTO Cinema (CinemaName, CLocation, AdminID)
VALUES ('Renaissance Cinema', 'Mall of Arabia, Giza', 'admin@email.com');

INSERT INTO Cinema (CinemaName, CLocation, AdminID)
VALUES ('Vox Cinemas', 'City Centre Almaza, Cairo', 'admin@email.com');

INSERT INTO Cinema (CinemaName, CLocation, AdminID)
VALUES ('Cineplex', 'Alexandria Corniche, Alexandria', 'admin@email.com');

INSERT INTO Cinema (CinemaName, CLocation, AdminID)
VALUES ('Point 90 Cinema', 'Point 90 Mall, New Cairo', 'admin@email.com');


select * from cinema



-- CinemaID = 1 (14 halls)
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('1', '2D', 1, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('2', '2D', 1, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('3', '3D', 1, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('4', '3D', 1, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('5', 'IM', 1, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('6', '2D', 1, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('7', '2D', 1, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('8', '3D', 1, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('9', 'IM', 1, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('10', '2D', 1, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('11', '3D', 1, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('12', '3D', 1, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('13', 'IM', 1, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('14', '2D', 1, 'admin@email.com');

-- CinemaID = 2 (10 halls)
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('1', '2D', 2, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('2', '2D', 2, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('3', '3D', 2, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('4', 'IM', 2, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('5', '2D', 2, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('6', '3D', 2, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('7', '2D', 2, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('8', '3D', 2, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('9', 'IM', 2, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('10', '2D', 2, 'admin@email.com');

-- CinemaID = 3 (12 halls)
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('1', '2D', 3, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('2', '3D', 3, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('3', 'IM', 3, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('4', '2D', 3, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('5', '2D', 3, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('6', '3D', 3, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('7', 'IM', 3, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('8', '2D', 3, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('9', '3D', 3, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('10', '3D', 3, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('11', 'IM', 3, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('12', '2D', 3, 'admin@email.com');

-- CinemaID = 4 (8 halls)
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('1', '2D', 4, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('2', '2D', 4, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('3', '3D', 4, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('4', 'IM', 4, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('5', '3D', 4, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('6', '2D', 4, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('7', '3D', 4, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('8', 'IM', 4, 'admin@email.com');

-- CinemaID = 5 (9 halls)
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('1', '2D', 5, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('2', '3D', 5, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('3', 'IM', 5, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('4', '2D', 5, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('5', '3D', 5, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('6', '3D', 5, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('7', 'IM', 5, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('8', '2D', 5, 'admin@email.com');
INSERT INTO Hall (HallName, ScreenType, CinemaID, AdminID) VALUES ('9', '3D', 5, 'admin@email.com');

EXEC CreateHallSeats '22', 10, 7, 1, 0, 'admin@email.com';
EXEC CreateHallSeats '23', 10, 7, 1, 0, 'admin@email.com';
EXEC CreateHallSeats '24', 10, 7, 1, 0, 'admin@email.com';
EXEC CreateHallSeats '35', 10, 7, 1, 0, 'admin@email.com';
EXEC CreateHallSeats '36', 10, 7, 1, 0, 'admin@email.com';
EXEC CreateHallSeats '42', 10, 7, 1, 0, 'admin@email.com';
EXEC CreateHallSeats '43', 10, 7, 1, 0, 'admin@email.com';
EXEC CreateHallSeats '44', 10, 7, 1, 0, 'admin@email.com';
EXEC CreateHallSeats '52', 10, 7, 1, 0, 'admin@email.com';
EXEC CreateHallSeats '53', 10, 7, 1, 0, 'admin@email.com';

-- Inception (MovieID 1)
INSERT INTO MoviesActors (MovieID, Actor) VALUES (1, 'Leonardo DiCaprio');
INSERT INTO MoviesActors (MovieID, Actor) VALUES (1, 'Joseph Gordon-Levitt');

-- Jurassic Park (MovieID 2)
INSERT INTO MoviesActors (MovieID, Actor) VALUES (2, 'Sam Neill');
INSERT INTO MoviesActors (MovieID, Actor) VALUES (2, 'Laura Dern');

-- Avatar (MovieID 3)
INSERT INTO MoviesActors (MovieID, Actor) VALUES (3, 'Sam Worthington');
INSERT INTO MoviesActors (MovieID, Actor) VALUES (3, 'Zoe Saldana');

-- The Lord of the Rings: The Fellowship of the Ring (MovieID 4)
INSERT INTO MoviesActors (MovieID, Actor) VALUES (4, 'Elijah Wood');
INSERT INTO MoviesActors (MovieID, Actor) VALUES (4, 'Ian McKellen');

-- Pulp Fiction (MovieID 5)
INSERT INTO MoviesActors (MovieID, Actor) VALUES (5, 'John Travolta');
INSERT INTO MoviesActors (MovieID, Actor) VALUES (5, 'Samuel L. Jackson');

-- The Godfather (MovieID 6)
INSERT INTO MoviesActors (MovieID, Actor) VALUES (6, 'Marlon Brando');
INSERT INTO MoviesActors (MovieID, Actor) VALUES (6, 'Al Pacino');

-- Star Wars: Episode IV - A New Hope (MovieID 7)
INSERT INTO MoviesActors (MovieID, Actor) VALUES (7, 'Mark Hamill');
INSERT INTO MoviesActors (MovieID, Actor) VALUES (7, 'Harrison Ford');

-- Gladiator (MovieID 8)
INSERT INTO MoviesActors (MovieID, Actor) VALUES (8, 'Russell Crowe');
INSERT INTO MoviesActors (MovieID, Actor) VALUES (8, 'Joaquin Phoenix');

-- The Wolf of Wall Street (MovieID 9)
INSERT INTO MoviesActors (MovieID, Actor) VALUES (9, 'Leonardo DiCaprio');
INSERT INTO MoviesActors (MovieID, Actor) VALUES (9, 'Jonah Hill');

-- Fight Club (MovieID 10)
INSERT INTO MoviesActors (MovieID, Actor) VALUES (10, 'Brad Pitt');
INSERT INTO MoviesActors (MovieID, Actor) VALUES (10, 'Edward Norton');

-- Forrest Gump (MovieID 11)
INSERT INTO MoviesActors (MovieID, Actor) VALUES (11, 'Tom Hanks');
INSERT INTO MoviesActors (MovieID, Actor) VALUES (11, 'Robin Wright');

-- The Dark Knight (MovieID 12)
INSERT INTO MoviesActors (MovieID, Actor) VALUES (12, 'Christian Bale');
INSERT INTO MoviesActors (MovieID, Actor) VALUES (12, 'Heath Ledger');

-- Dune (MovieID 13)
INSERT INTO MoviesActors (MovieID, Actor) VALUES (13, 'Timothée Chalamet');
INSERT INTO MoviesActors (MovieID, Actor) VALUES (13, 'Rebecca Ferguson');

-- Lady Bird (MovieID 14)
INSERT INTO MoviesActors (MovieID, Actor) VALUES (14, 'Saoirse Ronan');
INSERT INTO MoviesActors (MovieID, Actor) VALUES (14, 'Laurie Metcalf');

-- Parasite (MovieID 15)
INSERT INTO MoviesActors (MovieID, Actor) VALUES (15, 'Song Kang-ho');
INSERT INTO MoviesActors (MovieID, Actor) VALUES (15, 'Lee Sun-kyun');

-- Gravity (MovieID 16)
INSERT INTO MoviesActors (MovieID, Actor) VALUES (16, 'Sandra Bullock');
INSERT INTO MoviesActors (MovieID, Actor) VALUES (16, 'George Clooney');

-- Jojo Rabbit (MovieID 17)
INSERT INTO MoviesActors (MovieID, Actor) VALUES (17, 'Roman Griffin Davis');
INSERT INTO MoviesActors (MovieID, Actor) VALUES (17, 'Scarlett Johansson');

-- Get Out (MovieID 18)
INSERT INTO MoviesActors (MovieID, Actor) VALUES (18, 'Daniel Kaluuya');
INSERT INTO MoviesActors (MovieID, Actor) VALUES (18, 'Allison Williams');

-- The Shining (MovieID 19)
INSERT INTO MoviesActors (MovieID, Actor) VALUES (19, 'Jack Nicholson');
INSERT INTO MoviesActors (MovieID, Actor) VALUES (19, 'Shelley Duvall');

-- The Grand Budapest Hotel (MovieID 20)
INSERT INTO MoviesActors (MovieID, Actor) VALUES (20, 'Ralph Fiennes');
INSERT INTO MoviesActors (MovieID, Actor) VALUES (20, 'Tony Revolori');

-- Bookings for Amr (2 bookings)
INSERT INTO Booking (TotalPrice, BookingDate, CustomerID)
VALUES (120.50, '2025-04-15 18:30:00', 'amr@email.com');

INSERT INTO Booking (TotalPrice, BookingDate, CustomerID)
VALUES (85.75, '2025-04-28 20:15:00', 'amr@email.com');

-- Bookings for Sara (2 bookings)
INSERT INTO Booking (TotalPrice, BookingDate, CustomerID)
VALUES (95.25, '2025-04-10 19:45:00', 'sara@email.com');

INSERT INTO Booking (TotalPrice, BookingDate, CustomerID)
VALUES (150.00, '2025-04-23 21:00:00', 'sara@email.com');

-- Bookings for Mohamed (1 booking)
INSERT INTO Booking (TotalPrice, BookingDate, CustomerID)
VALUES (65.50, '2025-04-18 17:30:00', 'mohamed@email.com');

-- Bookings for Laila (2 bookings)
INSERT INTO Booking (TotalPrice, BookingDate, CustomerID)
VALUES (110.25, '2025-04-12 20:00:00', 'laila@email.com');

INSERT INTO Booking (TotalPrice, BookingDate, CustomerID)
VALUES (75.50, '2025-04-29 18:45:00', 'laila@email.com');

--42 ShowTimes inserted into the DB:
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-01 10:00:00', 'admin@email.com', 2, 6);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-01 13:00:00', 'admin@email.com', 2, 8);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-01 16:00:00', 'admin@email.com', 5, 11);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-01 19:00:00', 'admin@email.com', 5, 7);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-01 22:00:00', 'admin@email.com', 5, 2);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-02 01:00:00', 'admin@email.com', 8, 3);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-02 04:00:00', 'admin@email.com', 8, 4);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-02 07:00:00', 'admin@email.com', 11, 20);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-02 10:00:00', 'admin@email.com', 11, 8);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-02 13:00:00', 'admin@email.com', 14, 4);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-02 16:00:00', 'admin@email.com', 14, 3);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-02 19:00:00', 'admin@email.com', 14, 19);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-02 22:00:00', 'admin@email.com', 17, 11);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-03 01:00:00', 'admin@email.com', 17, 17);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-03 04:00:00', 'admin@email.com', 17, 14);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-03 07:00:00', 'admin@email.com', 20, 6);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-03 10:00:00', 'admin@email.com', 20, 1);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-03 13:00:00', 'admin@email.com', 23, 5);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-03 16:00:00', 'admin@email.com', 23, 17);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-03 19:00:00', 'admin@email.com', 23, 6);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-03 22:00:00', 'admin@email.com', 26, 5);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-04 01:00:00', 'admin@email.com', 26, 14);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-04 04:00:00', 'admin@email.com', 29, 17);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-04 07:00:00', 'admin@email.com', 29, 1);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-04 10:00:00', 'admin@email.com', 32, 9);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-04 13:00:00', 'admin@email.com', 32, 11);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-04 16:00:00', 'admin@email.com', 32, 4);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-04 19:00:00', 'admin@email.com', 35, 2);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-04 22:00:00', 'admin@email.com', 35, 15);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-05 01:00:00', 'admin@email.com', 35, 10);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-05 04:00:00', 'admin@email.com', 38, 1);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-05 07:00:00', 'admin@email.com', 38, 9);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-05 10:00:00', 'admin@email.com', 41, 14);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-05 13:00:00', 'admin@email.com', 41, 10);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-05 16:00:00', 'admin@email.com', 44, 7);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-05 19:00:00', 'admin@email.com', 44, 13);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-05 22:00:00', 'admin@email.com', 44, 16);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-06 01:00:00', 'admin@email.com', 47, 15);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-06 04:00:00', 'admin@email.com', 47, 1);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-06 07:00:00', 'admin@email.com', 50, 6);
INSERT INTO Showtimes (StartTime, AdminID, HallID, MovieID) VALUES ('2025-05-06 10:00:00', 'admin@email.com', 50, 20);

select * from showtimes;


-- Tickets for BookingID 1 (Amr)
INSERT INTO Ticket (BookingID, StartTime, SeatNumber, RowNumber, MovieID, HallID, Price)
VALUES 
(1, '2025-05-01 10:00:00', 1, 'A', 6, 2, 40.25),
(1, '2025-05-01 10:00:00', 2, 'A', 6, 2, 40.25),
(1, '2025-05-01 10:00:00', 3, 'A', 6, 2, 40.00);

-- BookingID 2 (Amr)
INSERT INTO Ticket (BookingID, StartTime, SeatNumber, RowNumber, MovieID, HallID, Price)
VALUES 
(2, '2025-05-01 13:00:00', 1, 'B', 8, 2, 42.00),
(2, '2025-05-01 13:00:00', 2, 'B', 8, 2, 43.75);

-- BookingID 3 (Sara)
INSERT INTO Ticket (BookingID, StartTime, SeatNumber, RowNumber, MovieID, HallID, Price)
VALUES 
(3, '2025-05-01 16:00:00', 1, 'C', 11, 5, 47.75),
(3, '2025-05-01 16:00:00', 2, 'C', 11, 5, 47.50);

-- BookingID 4 (Sara)
INSERT INTO Ticket (BookingID, StartTime, SeatNumber, RowNumber, MovieID, HallID, Price)
VALUES 
(4, '2025-05-01 22:00:00', 1, 'D', 2, 5, 50.00),
(4, '2025-05-01 22:00:00', 2, 'D', 2, 5, 50.00),
(4, '2025-05-01 22:00:00', 3, 'D', 2, 5, 50.00);

-- BookingID 5 (Mohamed)
INSERT INTO Ticket (BookingID, StartTime, SeatNumber, RowNumber, MovieID, HallID, Price)
VALUES 
(5, '2025-05-02 01:00:00', 1, 'A', 3, 8, 32.75),
(5, '2025-05-02 01:00:00', 2, 'A', 3, 8, 32.75);

-- BookingID 6 (Laila)
INSERT INTO Ticket (BookingID, StartTime, SeatNumber, RowNumber, MovieID, HallID, Price)
VALUES 
(6, '2025-05-02 07:00:00', 1, 'E', 20, 11, 55.25),
(6, '2025-05-02 07:00:00', 2, 'E', 20, 11, 55.00);

-- BookingID 7 (Laila)
INSERT INTO Ticket (BookingID, StartTime, SeatNumber, RowNumber, MovieID, HallID, Price)
VALUES 
(7, '2025-05-02 10:00:00', 1, 'C', 8, 11, 37.75),
(7, '2025-05-02 10:00:00', 2, 'C', 8, 11, 37.75);

select * from Ticket