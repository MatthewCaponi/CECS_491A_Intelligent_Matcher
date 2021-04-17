CREATE PROCEDURE [dbo].[UserScopeClaim_Update]
	@Id int,
	@userAccountId int,
	@scopeClaimId int
AS
begin
	set nocount on;

	update dbo.[UserScopeClaim]
	set userAccountId = @userAccountId,
	scopeClaimId = @scopeClaimId
	where Id = @Id;
end
