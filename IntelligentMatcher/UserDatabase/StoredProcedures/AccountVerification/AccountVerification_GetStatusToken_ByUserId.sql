CREATE PROCEDURE [dbo].[AccountVerification_GetStatusToken_ByUserId]
	@UserId int
AS
begin

	set nocount on

	select [Token]
	from dbo.[AccountVerification]
	where UserId = @UserId;
end