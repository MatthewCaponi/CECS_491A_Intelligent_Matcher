CREATE PROCEDURE [dbo].[UserAccountSettings_Update_FontSize]
	@UserId int,
	@FontSize int
AS
begin
	set nocount on;

	update dbo.[UserAccountSettings]
	set FontSize = @FontSize
	where UserId = @UserId;
end
