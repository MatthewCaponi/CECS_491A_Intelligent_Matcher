CREATE TABLE [dbo].[LoginTracker]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Username] NCHAR(50) NOT NULL, 
    [LoginTime] DATETIME NOT NULL
)
