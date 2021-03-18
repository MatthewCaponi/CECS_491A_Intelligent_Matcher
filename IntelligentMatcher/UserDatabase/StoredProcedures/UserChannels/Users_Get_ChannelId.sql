CREATE PROCEDURE [dbo].[Users_Get_ChannelId]
	@ChannelId int
AS
begin
	set nocount on;
	SELECT [UserId]
	from dbo.UserChannels
	WHERE ChannelId = @ChannelId;
end