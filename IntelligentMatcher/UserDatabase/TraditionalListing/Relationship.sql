CREATE TABLE [dbo].[Relationship]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [RelationshipType] NVARCHAR(50) NOT NULL, 
    [Age] INT NOT NULL, 
    [Interests] NVARCHAR(50) NOT NULL, 
    [GenderPreference] NVARCHAR(50) NOT NULL, 
    [ListingId] INT NOT NULL, 
    CONSTRAINT [Relationship_Listing_FK] FOREIGN KEY ([ListingId]) REFERENCES [Listing]([Id])
)
