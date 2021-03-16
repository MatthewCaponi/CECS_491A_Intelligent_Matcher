CREATE PROCEDURE [dbo].[UserAccountSettings_Get_All]
AS
begin
	set nocount on;
	SELECT [Id], [FontSize], [FontStyle], [ThemeColor], [UserId]
	from dbo.[UserAccountSettings];
end
