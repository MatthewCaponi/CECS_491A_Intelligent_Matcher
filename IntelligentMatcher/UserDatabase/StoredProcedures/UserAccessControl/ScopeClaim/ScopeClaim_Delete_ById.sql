CREATE PROCEDURE [dbo].[ScopeClaim_Delete_ById]
	@Id int
AS
begin
	set nocount on;

	delete
	from dbo.[ScopeClaim]
	where Id = @Id;

end