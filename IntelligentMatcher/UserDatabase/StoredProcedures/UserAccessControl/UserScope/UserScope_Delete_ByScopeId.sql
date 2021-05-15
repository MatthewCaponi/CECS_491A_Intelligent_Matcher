CREATE PROCEDURE [dbo].[UserScope_Delete_ByScopeId]
	@Id int
AS
begin
	set nocount on;

	delete
	from dbo.[UserScope]
	where Id = @Id;

end