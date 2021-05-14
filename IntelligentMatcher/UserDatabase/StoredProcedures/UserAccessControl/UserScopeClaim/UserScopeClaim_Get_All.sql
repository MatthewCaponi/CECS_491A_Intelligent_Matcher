CREATE PROCEDURE [dbo].[UserScopeClaim_Get_All]
AS
begin
	set nocount on;
	SELECT [Id], [userAccountId], [userScopeId], [userClaimId] [role]
	from dbo.[UserScopeClaim];
end