CREATE PROCEDURE [dbo].[Messages_Get_All_By_ChannelId]
	@ChannelId int

AS
begin
	set nocount on;
	SELECT [Id], [ChannelId], [ChannelMessageId], [UserId], [Message], [Time], [Date], [Username]
	from dbo.Messages
	WHERE ChannelId = @ChannelId;
end
