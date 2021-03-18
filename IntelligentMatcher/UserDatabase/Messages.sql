CREATE TABLE [dbo].[Messages]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ChannelId] INT NOT NULL, 
	[ChannelMessageId] INT NOT NULL, 
	[UserId] INT NOT NULL,	
	[Message] nvarchar(1000) NOT NULL,
	[Time] NVARCHAR(100) NOT NULL,
	[Username] nvarchar(100),
	[Date] date NOT NULL
)
