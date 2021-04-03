CREATE PROCEDURE [dbo].[LoginAttempts_UpdateSuspensionEndTime_ById]
	@Id int,
	@SuspensionEndTime datetimeoffset(7)
AS
begin
	set nocount on;

	update dbo.[LoginAttempts]
	set SuspensionEndTime = @SuspensionEndTime
	where Id = @Id;
end