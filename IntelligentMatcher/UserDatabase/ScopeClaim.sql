CREATE TABLE [dbo].[ScopeClaim]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [scopeId] INT NOT NULL, 
    [claimId] INT NOT NULL, 
    CONSTRAINT [FK_ScopeClaim_Scope] FOREIGN KEY ([scopeId]) REFERENCES [Scope]([Id])
    ON DELETE CASCADE, 
    CONSTRAINT [FK_ScopeClaim_Claim] FOREIGN KEY ([claimId]) REFERENCES [Claim]([Id])
    ON DELETE CASCADE
)
