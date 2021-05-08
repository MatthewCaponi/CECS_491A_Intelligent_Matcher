CREATE PROCEDURE [dbo].[LoginTracker_DeletebyId]
	@Id int 
AS
begin
	set nocount on;

	delete
	from dbo.[LoginTracker]
	where Id = @Id;

end