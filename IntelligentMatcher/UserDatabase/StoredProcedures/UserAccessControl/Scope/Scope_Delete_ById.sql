CREATE PROCEDURE [dbo].[Scope_Delete_ById]
	@Id int
AS
begin
	set nocount on;

	delete
	from dbo.[Scope]
	where Id = @Id;

end