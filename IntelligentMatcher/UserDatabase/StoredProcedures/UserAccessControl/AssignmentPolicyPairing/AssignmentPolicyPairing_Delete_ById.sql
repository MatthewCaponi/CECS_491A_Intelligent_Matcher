CREATE PROCEDURE [dbo].[AssignmentPolicyPairing_Delete_ById]
	@Id int
AS
begin
	set nocount on;

	delete
	from dbo.[AssignmentPolicyPairing]
	where Id = @Id;

end