CREATE TABLE [dbo].[AssignmentPolicyPairing]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [policyId] INT NOT NULL, 
    [scopeId] INT NOT NULL, 
    CONSTRAINT [FK_AssignmentPolicyPairing_AssignmentPolicy] FOREIGN KEY ([policyId]) REFERENCES [AssignmentPolicy]([Id])
    ON DELETE CASCADE, 
    CONSTRAINT [FK_AssignmentPolicyPairing_Scope] FOREIGN KEY ([scopeId]) REFERENCES [Scope]([Id])
    ON DELETE CASCADE
)
