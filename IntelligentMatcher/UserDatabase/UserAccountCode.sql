CREATE TABLE [dbo].[UserAccountCode]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Code] NVARCHAR(50) NOT NULL,
    [ExpirationTime] DATETIMEOFFSET NOT NULL,
    [UserAccountId] INT NOT NULL, 
    CONSTRAINT [FK_UserAccountCode_UserAccount] FOREIGN KEY (UserAccountId) REFERENCES [UserAccount]([Id])
    ON DELETE CASCADE
)