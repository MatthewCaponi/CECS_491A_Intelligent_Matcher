CREATE PROCEDURE [dbo].[Messages_Get_All]
AS
begin
	set nocount on;
	SELECT [Id], [ChannelId], [ChannelMessageId], [UserId], [Message], [Time], [Date], [Username]
	from dbo.Messages;
end
