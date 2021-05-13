CREATE PROCEDURE [dbo].[UserScopeClaim_Create]
	@userAccountId int,
	@scopeClaimId int,
	@role nvarchar(50),
	@Id int output
AS
begin
	set nocount on;

	insert into dbo.[UserScopeClaim]([userAccountId], [scopeClaimId], [role])
		values (@userAccountId, @scopeClaimId, @role);

	set @Id = SCOPE_IDENTITY();
end