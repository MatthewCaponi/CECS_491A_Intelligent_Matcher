﻿CREATE TABLE [dbo].[AccessPolicyPairing]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [scopeId] INT NOT NULL, 
    [claimId] INT NOT NULL, 
    [accessPolicyId] INT NOT NULL, 
    CONSTRAINT [FK_AccessPolicyPairing_Scope] FOREIGN KEY ([scopeId]) REFERENCES [Scope]([Id])
    ON DELETE CASCADE, 
    CONSTRAINT [FK_AccessPolicyPairing_Claim] FOREIGN KEY ([claimId]) REFERENCES [Claim]([Id])
    ON DELETE CASCADE, 
    CONSTRAINT [FK_AccessPolicyPairing_AccessPolicy] FOREIGN KEY ([accessPolicyId]) REFERENCES [AccessPolicy]([Id])
    ON DELETE CASCADE
)
