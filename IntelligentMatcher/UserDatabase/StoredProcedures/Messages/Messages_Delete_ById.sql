CREATE PROCEDURE [dbo].[Messages_Delete_ById]
	@Id int
AS
begin
	set nocount on;

	delete 
	from dbo.[Messages] 
	where Id = @Id;

end