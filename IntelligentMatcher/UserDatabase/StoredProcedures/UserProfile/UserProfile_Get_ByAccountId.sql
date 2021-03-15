CREATE PROCEDURE [dbo].[UserProfile_Get_ByAccountId]
	@UserAccountId int
AS
begin
	set nocount on

	SELECT [Id], [FirstName], [Surname], [DateOfBirth], [UserAccountId]
	from dbo.[UserProfile]
	where UserAccountId = @UserAccountId;
end