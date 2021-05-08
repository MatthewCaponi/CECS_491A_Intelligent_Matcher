CREATE PROCEDURE [dbo].[DeleteRelationship]
	@Id int 
AS
begin
	set nocount on;

	delete
	from dbo.[Relationship]
	where Id = @Id;

end
