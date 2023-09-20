INSERT INTO Movies(Name, Description, Price, ImageURL, StartDate, EndDate, CinemaId, DirectorId, MovieCategory)
VALUES
('Life', 'This is the Life movie description', 39.50, 'http://dotnethow.net/images/movies/movie-3.jpeg', SYSDATETIME(), DATEADD(day, 10, SYSDATETIME()), 3, 3, 4),
('The Shawshank Redemption', 'This is the The Shawshank Redemption movie description', 29.50, 'http://dotnethow.net/images/movies/movie-1.jpeg', SYSDATETIME(), DATEADD(day, 3, SYSDATETIME()), 1, 1, 1),
('Ghost', 'This is the Ghost movie description', 39.50, 'http://dotnethow.net/images/movies/movie-4.jpeg', SYSDATETIME(), DATEADD(day, 7, SYSDATETIME()), 4, 4, 6),
('Race', 'This is the Race movie description', 39.50, 'http://dotnethow.net/images/movies/movie-6.jpeg', SYSDATETIME(), DATEADD(day, -5, SYSDATETIME()), 1, 2, 4),
('Scoob', 'This is the Scoob movie description', 39.50, 'http://dotnethow.net/images/movies/movie-7.jpeg', SYSDATETIME(), DATEADD(day, -2, SYSDATETIME()), 1, 3, 5),
('Cold Soles', 'This is the Cold Soles movie description', 39.50, 'http://dotnethow.net/images/movies/movie-8.jpeg', SYSDATETIME(), DATEADD(day, 20, SYSDATETIME()), 1, 5, 3)