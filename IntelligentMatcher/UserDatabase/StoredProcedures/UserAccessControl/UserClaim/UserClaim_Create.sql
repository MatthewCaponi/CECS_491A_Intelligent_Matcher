CREATE PROCEDURE [dbo].[UserClaim_Create]
	@Type nvarchar(50),
	@Value nvarchar(50),
	@UserAccountId int,
	@ID int output
AS
begin
	set nocount on;

	insert into dbo.[UserClaim]([Type], [Value], [UserAccountId])
		values (@Type, @Value, @UserAccountId);

	set @Id = SCOPE_IDENTITY();
end