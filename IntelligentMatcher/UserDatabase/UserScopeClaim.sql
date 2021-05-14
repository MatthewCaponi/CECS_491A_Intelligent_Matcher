CREATE TABLE [dbo].[UserScopeClaim]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [userAccountId] INT NOT NULL, 
    [userScopeId] INT NOT NULL, 
    [userClaimId] INT NOT NULL, 
    [role] NCHAR(50) NOT NULL, 
    CONSTRAINT [FK_UserScopeClaim_UserAccount] FOREIGN KEY ([userAccountId]) REFERENCES [UserAccount]([Id]), 
    CONSTRAINT [FK_UserScopeClaim_UserScope] FOREIGN KEY ([UserScopeId]) REFERENCES [UserScope]([Id]), 
    CONSTRAINT [FK_UserScopeClaim_UserClaim] FOREIGN KEY ([UserClaimId]) REFERENCES [UserClaim]([Id]) 
)
