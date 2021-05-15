CREATE TABLE [dbo].[Scope]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [type] NVARCHAR(50) NOT NULL, 
    [description] NVARCHAR(200) NULL, 
    [isDefault] BIT NULL
)
