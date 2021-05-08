CREATE TABLE [dbo].[PageVisitTracker]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [PageVisitedName] NVARCHAR(50) NOT NULL, 
    [PageVisitTime] DATETIME NOT NULL, 
    [UserId] INT NOT NULL
)
