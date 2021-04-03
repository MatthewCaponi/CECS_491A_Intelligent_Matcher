CREATE TABLE [dbo].[LoginAttempts]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[IpAddress] NVARCHAR(50) NOT NULL,
	[LoginCounter] INT NOT NULL,
	[SuspensionEndTime] DATETIMEOFFSET NOT NULL,
	CONSTRAINT [CK_LoginAttempts_IpAddress] UNIQUE (IpAddress)
)