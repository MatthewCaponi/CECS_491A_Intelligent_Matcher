CREATE PROCEDURE [dbo].[UserScopeClaim_Delete_ById]
	@Id int
AS
begin
	set nocount on;

	delete
	from dbo.[UserScopeClaim]
	where Id = @Id;

end