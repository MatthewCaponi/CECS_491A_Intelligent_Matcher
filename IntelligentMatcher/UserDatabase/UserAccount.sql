CREATE TABLE [dbo].[UserAccount]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Username] NVARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(50) NOT NULL, 
    [EmailAddress] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [CK_UserAccount_Username] UNIQUE (Username),
    CONSTRAINT [CK_UserAccount_EmailAddress] UNIQUE (EmailAddress)
)
