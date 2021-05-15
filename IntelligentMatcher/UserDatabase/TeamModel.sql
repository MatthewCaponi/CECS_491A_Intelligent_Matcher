﻿CREATE TABLE [dbo].[TeamModel]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TeamType] NVARCHAR(50) NOT NULL, 
    [GameType] NVARCHAR(50) NOT NULL, 
    [Platform] NVARCHAR(50) NOT NULL, 
    [Experience] NVARCHAR(50) NULL, 
    [ListingId] INT NOT NULL
)
