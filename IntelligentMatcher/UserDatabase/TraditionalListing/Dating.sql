CREATE TABLE [dbo].[Dating]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [LookingFor] NVARCHAR(50) NOT NULL, 
    [SexualOrientationPreference] NVARCHAR(50) NOT NULL, 
    [RelationshipId] INT NOT NULL, 
    CONSTRAINT [Dating_Relationship_FK] FOREIGN KEY ([RelationshipId]) REFERENCES [Relationship]([Id])
)
