CREATE PROCEDURE [dbo].[UserScope_Update]
	@Id int,
	@Type nvarchar(50),
	@UserAccountId int
AS
begin
	set nocount on;

	update dbo.[UserScope]
	set type = @Type,
	UserAccountId = @UserAccountId
	where Id = @Id;
end
