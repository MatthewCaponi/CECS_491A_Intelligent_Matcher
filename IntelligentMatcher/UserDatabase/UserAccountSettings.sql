CREATE TABLE [dbo].[UserAccountSettings]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[FontSize] INT  NOT NULL, 
	[FontStyle] nvarchar(50) NOT NULL, 
	[ThemeColor] nvarchar(50) NOT NULL, 
	[UserId] INT NOT NULL, 
    CONSTRAINT [FK_UserAccountSettings_UserAccount] FOREIGN KEY (UserId) REFERENCES [dbo].[UserAccount]([Id])

)
