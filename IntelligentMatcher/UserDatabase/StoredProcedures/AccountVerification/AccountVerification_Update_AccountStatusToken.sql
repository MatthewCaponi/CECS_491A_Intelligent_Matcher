CREATE PROCEDURE [dbo].[AccountVerification_Update_AccountStatusToken]
	@UserId int,
	@Token nvarchar(200)
AS
begin
	set nocount on;

	update dbo.[AccountVerification]
	set Token = @Token
	where UserId = @UserId;
end
