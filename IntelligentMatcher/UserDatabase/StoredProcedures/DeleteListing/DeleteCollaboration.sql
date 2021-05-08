CREATE PROCEDURE [dbo].[DeleteCollaboration]
	@Id int 
AS
begin
	set nocount on;

	delete
	from dbo.[Collaboration]
	where Id = @Id;

end
