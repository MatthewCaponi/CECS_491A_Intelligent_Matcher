CREATE PROCEDURE [dbo].[SearchTracker_DeletebyId]
	@Id int 
AS
begin
	set nocount on;

	delete
	from dbo.[ListingSearchTracker]
	where Id = @Id;

end