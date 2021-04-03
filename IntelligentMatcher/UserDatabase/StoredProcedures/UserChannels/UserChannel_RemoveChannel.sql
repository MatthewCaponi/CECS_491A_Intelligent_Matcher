CREATE PROCEDURE [dbo].[UserChannel_RemoveChannel]
	@ChannelId int
AS
begin
	set nocount on;

	delete
	from dbo.[UserChannels]
	where ChannelId = @ChannelId;

end