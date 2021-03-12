CREATE PROCEDURE [dbo].[UserAccountSettings_Update_ThemeColor]
	@UserId int,
	@ThemeColor nvarchar(50)
AS
begin
	set nocount on;

	update dbo.[UserAccountSettings]
	set ThemeColor = @ThemeColor
	where UserId = @UserId;
end
