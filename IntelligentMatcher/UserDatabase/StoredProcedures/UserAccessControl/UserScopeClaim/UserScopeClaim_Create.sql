CREATE PROCEDURE [dbo].[UserScopeClaim_Create]
	@userAccountId int,
	@userScopeId int,
	@userClaimId int,
	@role nvarchar(50),
	@Id int output
AS
begin
	set nocount on;

	insert into dbo.[UserScopeClaim]([userAccountId], [userScopeId], [userClaimId], [role])
		values (@userAccountId, @userScopeId, @userClaimId, @role);

	set @Id = SCOPE_IDENTITY();
end