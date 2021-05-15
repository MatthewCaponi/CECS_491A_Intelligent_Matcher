CREATE PROCEDURE [dbo].[UserScope_Get_ByScopeId]
	@scopeId int
AS
begin
	set nocount on;
	SELECT [Id], [Type], [UserAccountId]
	from dbo.[UserScope]
	where Id = @scopeId
end