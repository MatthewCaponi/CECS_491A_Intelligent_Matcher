CREATE TABLE [dbo].[Listing]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(50) NOT NULL, 
    [Details] NVARCHAR(1000) NOT NULL, 
    [City] NVARCHAR(50) NULL, 
    [State] NVARCHAR(50) NULL, 
    [NumberOfParticipants] INT NOT NULL, 
    [InPersonOrRemote] NVARCHAR(50) NOT NULL, 
    [UserAccountID] INT NOT NULL, 
    CONSTRAINT [Listing_UserAccount_FK] FOREIGN KEY ([Id]) REFERENCES [UserAccount]([Id]) 
    ON DELETE CASCADE
    
    
)
