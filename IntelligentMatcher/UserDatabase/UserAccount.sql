CREATE TABLE [dbo].[UserAccount]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Username] NVARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(50) NOT NULL, 
    [Salt] NVARCHAR(100) NOT NULL, 
    [EmailAddress] NVARCHAR(50) NOT NULL, 
    [AccountType] NVARCHAR(50) NOT NULL, 
    [AccountStatus] NVARCHAR(50) NOT NULL, 
    [CreationDate] DATE NOT NULL, 
    [UpdationDate] DATE NOT NULL, 
    CONSTRAINT [CK_UserAccount_Username] UNIQUE (Username)
)
