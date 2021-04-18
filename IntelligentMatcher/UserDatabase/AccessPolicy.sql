CREATE TABLE [dbo].[AccessPolicy]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [name] NVARCHAR(50) NOT NULL, 
    [priority] INT NOT NULL, 
    [resourceId] INT NOT NULL, 
    CONSTRAINT [FK_AccessPolicy_Resource] FOREIGN KEY ([resourceId]) REFERENCES [Resource]([Id])
    ON DELETE CASCADE
)
