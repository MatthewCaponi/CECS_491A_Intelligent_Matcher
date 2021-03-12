CREATE PROCEDURE [dbo].[UserAccountSettings_Update_FontStyle]
	@UserId int,
	@FontStyle nvarchar(50)
AS
begin
	set nocount on;

	update dbo.[UserAccountSettings]
	set FontStyle = @FontStyle
	where UserId = @UserId;
end
