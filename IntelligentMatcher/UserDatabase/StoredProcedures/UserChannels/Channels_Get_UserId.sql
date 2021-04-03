CREATE PROCEDURE [dbo].[Channels_Get_UserId]
	@UserId int
AS
begin
	set nocount on;
	SELECT [ChannelId]
	from dbo.UserChannels
	WHERE UserId = @UserId;
end