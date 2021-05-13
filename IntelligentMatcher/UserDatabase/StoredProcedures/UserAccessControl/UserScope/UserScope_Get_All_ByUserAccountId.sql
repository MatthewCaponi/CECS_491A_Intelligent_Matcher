CREATE PROCEDURE [dbo].[UserScope_Get_All_ByUserAccountId]
	@userAccountId int
AS
begin
	set nocount on;
	SELECT [Id], [Type], [UserAccountId]
	from dbo.[UserScope]
	where UserAccountId = @userAccountId
end