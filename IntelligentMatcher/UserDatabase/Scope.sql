CREATE TABLE [dbo].[Scope]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [name] NVARCHAR(50) NOT NULL, 
    [description] NVARCHAR(200) NOT NULL, 
    [isDefault] BIT NOT NULL
)
