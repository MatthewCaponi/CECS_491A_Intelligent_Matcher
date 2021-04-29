CREATE PROCEDURE [dbo].[UserReports_Delete]
	@Id int
AS
begin
	set nocount on;

	delete 
	from dbo.[UserReports] 
	where Id = @Id;
end