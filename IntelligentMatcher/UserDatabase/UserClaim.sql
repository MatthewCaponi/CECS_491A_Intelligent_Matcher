CREATE TABLE [dbo].[UserClaim]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Type] NVARCHAR(50) NOT NULL, 
    [Value] NVARCHAR(50) NOT NULL, 
    [UserAccountId] INT NOT NULL, 
    CONSTRAINT [FK_UserClaim_UserAccount] FOREIGN KEY ([UserAccountId]) REFERENCES [UserAccount]([Id])
    ON DELETE CASCADE
)
