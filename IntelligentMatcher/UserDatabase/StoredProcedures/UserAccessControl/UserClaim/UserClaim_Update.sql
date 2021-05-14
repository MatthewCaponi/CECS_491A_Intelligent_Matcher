CREATE PROCEDURE [dbo].[UserClaim_Update]
	@Id int,
	@Type nvarchar(50),
	@Value nvarchar(50),
	@UserAccountId int
AS
begin
	set nocount on;

	update dbo.[UserClaim]
	set type = @Type,
	Value = @value,
	UserAccountId = @UserAccountId
	where Id = @Id;
end
