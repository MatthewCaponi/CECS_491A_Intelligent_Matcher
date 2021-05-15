CREATE TABLE [dbo].[UserScope]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Type] NVARCHAR(50) NOT NULL, 
    [UserAccountId] INT NOT NULL, 
    CONSTRAINT [FK_UserScope_UserAccount] FOREIGN KEY ([UserAccountId]) REFERENCES [UserAccount]([Id])
    ON DELETE CASCADE
)
