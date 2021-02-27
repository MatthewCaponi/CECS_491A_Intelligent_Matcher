CREATE TABLE [dbo].[UserInformation]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [DateOfBirth] DATE NOT NULL, 
    [EmailAddress] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [CK_UserInformation_EmailAddress] UNIQUE (EmailAddress)
)
