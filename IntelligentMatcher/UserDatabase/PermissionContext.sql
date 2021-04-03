CREATE TABLE [dbo].[PermissionContext]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [userPermissionId] INT NOT NULL, 
    [contextId] INT NOT NULL, 
    CONSTRAINT [FK_PermissionContext_UserPermission] FOREIGN KEY ([userPermissionId]) REFERENCES [UserPermission]([Id]), 
    CONSTRAINT [FK_PermissionContext_Context] FOREIGN KEY ([contextId]) REFERENCES [Context]([Id])
)
