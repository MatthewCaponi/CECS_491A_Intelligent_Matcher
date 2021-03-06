﻿CREATE TABLE [dbo].[Collaboration]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CollaborationType] NVARCHAR(50) NOT NULL, 
    [InvolvementType] NVARCHAR(50) NOT NULL, 
    [Experience] NVARCHAR(50) NULL, 
    [ListingId] INT NOT NULL, 
    CONSTRAINT [Collaboration_Listing_FK] FOREIGN KEY ([ListingId]) REFERENCES [Listing]([Id]) 
    ON DELETE CASCADE
)
