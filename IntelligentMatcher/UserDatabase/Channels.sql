﻿CREATE TABLE [dbo].[Channels]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[OwnerId] INT NOT NULL,
	[Name] NVARCHAR(100) NOT NULL
	
)