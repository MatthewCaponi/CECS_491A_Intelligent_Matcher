CREATE PROCEDURE [dbo].[LoginAttempts_ResetLoginCounter_ById]
	@Id int
AS
begin
	set nocount on;

	update dbo.[LoginAttempts]
	set LoginCounter = 0
	where Id = @Id;
end