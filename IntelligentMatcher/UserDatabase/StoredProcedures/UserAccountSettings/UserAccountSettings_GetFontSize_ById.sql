CREATE PROCEDURE [dbo].[UserAccountSettings_GetFontSize_ById]
	@UserId int
AS
begin
	set nocount on

	SELECT [FontSize]
	from dbo.[UserAccountSettings]
	where UserId = @UserId;
end