CREATE TABLE [dbo].[Claim]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [type] NVARCHAR(50) NOT NULL, 
    [value] NVARCHAR(50) NOT NULL, 
    [isDefault] BIT NOT NULL
)
