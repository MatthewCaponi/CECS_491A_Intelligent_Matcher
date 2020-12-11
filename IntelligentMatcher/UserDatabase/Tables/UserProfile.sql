CREATE TABLE [dbo].[UserProfile]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [DateOfBirth] DATE NOT NULL, 
    [AccountCreationDate] DATE NOT NULL, 
    [AccountType] VARCHAR(10) NOT NULL CHECK ([AccountType] IN('Admin', 'User')), 
    [AccountStatus] NCHAR(10) NOT NULL CHECK ([AccountStatus] IN('Active', 'Suspended', 'Banned', 'Disabled', 'Deleted')), 
    [UserAccountId] INT NULL, 
    CONSTRAINT [FK_UserProfile_UserAccount] FOREIGN KEY ([UserAccountId]) REFERENCES [UserAccount]([Id])

)
