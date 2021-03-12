CREATE PROCEDURE [dbo].[UserProfile_Delete_ById]
	@UserAccountId int
AS
begin
	set nocount on;

	delete
	from dbo.[UserProfile]
	where UserAccountId = @UserAccountId;

end