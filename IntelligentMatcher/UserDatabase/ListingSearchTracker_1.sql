CREATE TABLE [dbo].[ListingSearchTracker]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Search] NCHAR(1000) NOT NULL, 
    [SearchTime] DATETIME NOT NULL, 
    [UserId] INT NOT NULL
)
