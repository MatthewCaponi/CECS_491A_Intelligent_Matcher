CREATE PROCEDURE [dbo].[Resource_Delete_ById]
	@Id int
AS
begin
	set nocount on;

	delete
	from dbo.[Resource]
	where Id = @Id;

end