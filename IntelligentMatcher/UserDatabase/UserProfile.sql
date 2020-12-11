CREATE TABLE [dbo].[UserProfile]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [DateOfBirth] DATE NOT NULL, 
    [AccountCreationDate] DATE NOT NULL, 
    [AccountType] VARCHAR(10) NOT NULL CHECK ([AccountType] IN('Admin', 'User')),
    [AccountStatus] VARCHAR(10) NOT NULL CHECK ([AccountStatus] IN('Active', 'Disabled', 'Suspended', 'Banned', 'Deleted')), 
    [UserAccountId] INT NOT NULL, 
    CONSTRAINT [FK_UserProfile_UserAccount] FOREIGN KEY ([UserAccountId]) REFERENCES [UserAccount]([Id])
    ON DELETE CASCADE
)
