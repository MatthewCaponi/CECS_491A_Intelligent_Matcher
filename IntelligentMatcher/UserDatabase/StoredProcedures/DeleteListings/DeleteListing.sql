CREATE PROCEDURE [dbo].[DeleteListing]
	@Id int 
AS
begin
	set nocount on;

	delete
	from dbo.[Listing]
	where Id = @Id;

end
