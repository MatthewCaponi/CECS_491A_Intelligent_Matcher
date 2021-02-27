CREATE TABLE [dbo].[UserProfile]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [Surname] NVARCHAR(50) NOT NULL, 
    [UserAccountId] INT NOT NULL, 
    CONSTRAINT [FK_UserProfile_UserAccount] FOREIGN KEY ([UserAccountId]) REFERENCES [UserAccount]([Id])
    ON DELETE CASCADE
)
