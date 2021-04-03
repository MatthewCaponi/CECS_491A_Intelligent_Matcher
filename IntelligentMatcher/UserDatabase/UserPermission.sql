CREATE TABLE [dbo].[UserPermission]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [permissionId] INT NOT NULL, 
    [userId] INT NOT NULL, 
    CONSTRAINT [FK_UserPermission_Permission] FOREIGN KEY ([permissionId]) REFERENCES [Permission]([Id]), 
    CONSTRAINT [FK_UserPermission_UserAccount] FOREIGN KEY ([userId]) REFERENCES [UserAccount]([Id])
)
