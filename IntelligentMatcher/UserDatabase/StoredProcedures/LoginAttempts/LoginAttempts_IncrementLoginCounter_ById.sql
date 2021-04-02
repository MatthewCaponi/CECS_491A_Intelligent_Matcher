CREATE PROCEDURE [dbo].[LoginAttempts_IncrementLoginCounter_ById]
	@Id int
AS
begin
	set nocount on;

	update dbo.[LoginAttempts]
	set LoginCounter = LoginCounter + 1
	where Id = @Id;
end