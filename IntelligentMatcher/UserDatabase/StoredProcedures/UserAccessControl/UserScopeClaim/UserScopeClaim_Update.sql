CREATE PROCEDURE [dbo].[UserScopeClaim_Update]
	@Id int,
	@userAccountId int,
	@userScopeId int,
	@userClaimId int,
	@role nvarchar(50)
AS
begin
	set nocount on;

	update dbo.[UserScopeClaim]
	set userAccountId = @userAccountId,
	userScopeId = @userScopeId,
	userClaimId = @userClaimId,
	role = @role
	where Id = @Id;
end
