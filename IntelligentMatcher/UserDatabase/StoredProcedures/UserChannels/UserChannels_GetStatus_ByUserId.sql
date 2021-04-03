CREATE PROCEDURE [dbo].[UserChannels_GetStatus_ByUserId]
	@UserId int
AS
begin

	set nocount on

	select [AccountStatus]
	from dbo.[UserChannels]
	where UserId = @UserId;
end