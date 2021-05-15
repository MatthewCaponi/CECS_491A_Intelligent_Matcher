CREATE PROCEDURE [dbo].[UserScopeClaim_Get_All_ByAccountIdAndRole]
	@accountId int,
	@role nvarchar(50)
AS
begin
	set nocount on;
	SELECT [Id], [userAccountId], [userScopeId], [userClaimId], [role]
	from dbo.[UserScopeClaim]
	where userAccountId = @accountId
	and role = @role;
end