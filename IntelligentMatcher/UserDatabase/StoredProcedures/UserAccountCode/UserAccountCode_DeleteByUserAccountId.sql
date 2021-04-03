CREATE PROCEDURE [dbo].[UserAccountCode_Delete_ByUserAccountId]
	@UserAccountId int
AS
begin
	set nocount on;

	delete
	from dbo.[UserAccountCode]
	where UserAccountId = @UserAccountId;

end