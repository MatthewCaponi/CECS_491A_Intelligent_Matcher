CREATE PROCEDURE [dbo].[PageVistTracker_DeletebyId]
	@Id int 
AS
begin
	set nocount on;

	delete
	from dbo.[PageVisitTracker]
	where Id = @Id;

end