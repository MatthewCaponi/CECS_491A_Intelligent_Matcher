CREATE PROCEDURE [dbo].[UserChannel_UpdateStatus_Id]
	@UserId int,
	@AccountStatus nvarchar(20)
AS
begin
	set nocount on;

	update dbo.[UserChannels]
	set AccountStatus = @AccountStatus
	where UserId = @UserId;
end
