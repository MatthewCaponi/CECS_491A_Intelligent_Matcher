CREATE PROCEDURE [dbo].[UserScopeClaim_Create]
	@userAccountId int,
	@scopeClaimId int,
	@Id int output
AS
begin
	set nocount on;

	insert into dbo.[UserScopeClaim]([userAccountId], [scopeClaimId])
		values (@userAccountId, @scopeClaimId);

	set @Id = SCOPE_IDENTITY();
end