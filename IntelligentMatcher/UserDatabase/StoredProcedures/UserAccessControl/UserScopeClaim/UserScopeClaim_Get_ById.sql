CREATE PROCEDURE [dbo].[UserScopeClaim_Get_ById]
	@Id int
AS
begin

	set nocount on

	SELECT [Id], [userAccountId], [scopeClaimId], [role]
	from dbo.[UserScopeClaim]
	where Id = @Id;
end