CREATE TABLE [dbo].[Messages]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ChannelId] INT NOT NULL, 
	[ChannelMessageId] INT NOT NULL, 
	[UserId] INT NOT NULL,	
	[Message] nvarchar(1000) NOT NULL, 
    [Time] DATE NOT NULL

)
