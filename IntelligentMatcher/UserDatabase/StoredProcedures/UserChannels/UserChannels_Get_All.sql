CREATE PROCEDURE [dbo].[UserChannels_Get_All]
AS
begin
	set nocount on;
	SELECT [Id], [ChannelId],  [UserId]
	from dbo.UserChannels;
end
