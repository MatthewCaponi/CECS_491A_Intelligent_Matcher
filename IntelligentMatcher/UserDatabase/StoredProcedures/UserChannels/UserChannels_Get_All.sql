CREATE PROCEDURE [dbo].[UserChannels_Get_All]
AS
begin
	set nocount on;
	SELECT [Id], [ChannelId],  [UserId], [AccountStatus]
	from dbo.UserChannels;
end
