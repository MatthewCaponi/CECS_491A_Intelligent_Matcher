CREATE PROCEDURE [dbo].[AccessPolicy_Delete_ById]
	@Id int
AS
begin
	set nocount on;

	delete
	from dbo.[AccessPolicy]
	where Id = @Id;

end