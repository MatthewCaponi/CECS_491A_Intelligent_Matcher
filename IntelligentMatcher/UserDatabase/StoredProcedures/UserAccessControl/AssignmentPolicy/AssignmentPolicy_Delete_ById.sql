CREATE PROCEDURE [dbo].[AssignmentPolicy_Delete_ById]
	@Id int
AS
begin
	set nocount on;

	delete
	from dbo.[AssignmentPolicy]
	where Id = @Id;

end