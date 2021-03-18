CREATE PROCEDURE [dbo].[UserChannel_RemoveUser]
	@UserId int,
	@ChannelId int
AS
begin
	set nocount on;

	delete
	from dbo.[UserChannels]
	where UserId = @UserId AND ChannelId = @ChannelId;

end