CREATE PROCEDURE [dbo].[UserAccountSettings_Delete_ById]
	@UserId int
AS
begin
	set nocount on;

	delete
	from dbo.[UserAccountSettings]
	where UserId = @UserId;

end