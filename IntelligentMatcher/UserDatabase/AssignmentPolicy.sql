CREATE TABLE [dbo].[AssignmentPolicy]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [name] NVARCHAR(50) NOT NULL, 
    [default] BIT NOT NULL, 
    [requiredAccountType] NVARCHAR(50) NOT NULL, 
    [priority] INT NOT NULL
)
