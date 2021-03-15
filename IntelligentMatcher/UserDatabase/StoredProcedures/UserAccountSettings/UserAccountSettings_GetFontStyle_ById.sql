CREATE PROCEDURE [dbo].[UserAccountSettings_GetFontStyle_ById]
	@UserId int
AS
begin
	set nocount on

	SELECT [FontStyle]
	from dbo.[UserAccountSettings]
	where UserId = @UserId;
end