CREATE PROCEDURE [dbo].[AccessPolicyPairing_Delete_ById]
	@Id int
AS
begin
	set nocount on;

	delete
	from dbo.[AccessPolicyPairing]
	where Id = @Id;

end