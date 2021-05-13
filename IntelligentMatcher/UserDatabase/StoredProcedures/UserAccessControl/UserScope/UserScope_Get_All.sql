CREATE PROCEDURE [dbo].[UserScope_Get_All]
AS
begin
	set nocount on;
	SELECT [Id], [Type], [UserAccountId]
	from dbo.[UserScope];
end