CREATE PROCEDURE [dbo].[LoginAttempts_Delete_ById]
	@Id int
AS
begin
	set nocount on;

	delete
	from dbo.[LoginAttempts]
	where Id = @Id;

end