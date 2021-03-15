CREATE PROCEDURE [dbo].[UserAccountSettings_GetThemeColor_ById]
	@UserId int
AS
begin
	set nocount on

	SELECT [ThemeColor]
	from dbo.[UserAccountSettings]
	where UserId = @UserId;
end