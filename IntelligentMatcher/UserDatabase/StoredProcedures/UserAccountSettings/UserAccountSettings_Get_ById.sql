CREATE PROCEDURE [dbo].[UserAccountSettings_Get_ById]
	@UserId int
AS
begin
	set nocount on

	SELECT [Id], [FontSize], [FontStyle], [ThemeColor], [UserId]
	from dbo.[UserAccountSettings]
	where UserId = @UserId;
end