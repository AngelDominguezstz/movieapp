-- Crear la base de datos
CREATE DATABASE MoviesDB;
GO

-- Usar la base de datos
USE MoviesDB;
GO

-- Crear tabla Director
CREATE TABLE Director (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(200) NOT NULL,
    Nationality VARCHAR(100),
    Age INT,
    Active BIT
);

-- Crear tabla Movies
CREATE TABLE Movies (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL,
    ReleaseYear DATE,
    Gender VARCHAR(50),
    Duration TIME,
    FKDirector INT,
    CONSTRAINT FK_Movies_Director FOREIGN KEY (FKDirector)
        REFERENCES Director(Id)
);


INSERT INTO Director (Name, Nationality, Age, Active)
VALUES 
('Christopher Nolan', 'British-American', 53, 1),
('Quentin Tarantino', 'American', 61, 1),
('Hayao Miyazaki', 'Japanese', 83, 1),
('Alfonso Cuarón', 'Mexican', 62, 1),
('Francis Ford Coppola', 'American', 86, 0);

INSERT INTO Movies (Name, ReleaseYear, Gender, Duration, FKDirector)
VALUES
('Inception', '2010-07-16', 'Sci-Fi', '02:28:00', 1),
('Pulp Fiction', '1994-10-14', 'Crime', '02:34:00', 2),
('Spirited Away', '2001-07-20', 'Animation', '02:05:00', 3),
('Roma', '2018-08-25', 'Drama', '02:15:00', 4),
('The Godfather', '1972-03-24', 'Crime', '02:55:00', 5);
