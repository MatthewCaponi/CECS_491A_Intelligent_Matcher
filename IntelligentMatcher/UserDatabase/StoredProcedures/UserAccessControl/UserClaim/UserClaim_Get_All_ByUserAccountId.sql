CREATE PROCEDURE [dbo].[UserClaim_Get_All_ByUserAccountId]
	@userAccountId int
AS
begin
	set nocount on;
	SELECT [Id], [Type], [Value], [UserAccountId]
	from dbo.[UserClaim]
	where UserAccountId = @userAccountId
end