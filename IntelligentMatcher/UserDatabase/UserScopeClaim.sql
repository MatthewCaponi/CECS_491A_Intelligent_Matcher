CREATE TABLE [dbo].[UserScopeClaim]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [userAccountId] INT NOT NULL, 
    [scopeClaimId] INT NOT NULL, 
    CONSTRAINT [FK_UserScopeClaim_UserAccount] FOREIGN KEY ([userAccountId]) REFERENCES [UserAccount]([Id]), 
    CONSTRAINT [FK_UserScopeClaim_ScopeClaim] FOREIGN KEY ([scopeClaimId]) REFERENCES [ScopeClaim]([Id])
)
