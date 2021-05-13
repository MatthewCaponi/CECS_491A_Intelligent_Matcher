CREATE PROCEDURE [dbo].[UserScopeClaim_Update]
	@Id int,
	@userAccountId int,
	@scopeClaimId int,
	@role nvarchar(50)
AS
begin
	set nocount on;

	update dbo.[UserScopeClaim]
	set userAccountId = @userAccountId,
	scopeClaimId = @scopeClaimId,
	role = @role
	where Id = @Id;
end
