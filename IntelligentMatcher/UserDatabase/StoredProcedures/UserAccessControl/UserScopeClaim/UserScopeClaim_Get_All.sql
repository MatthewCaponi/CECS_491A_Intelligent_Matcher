CREATE PROCEDURE [dbo].[UserScopeClaim_Get_All]
AS
begin
	set nocount on;
	SELECT [Id], [userAccountId], [scopeClaimId], [role]
	from dbo.[UserScopeClaim];
end