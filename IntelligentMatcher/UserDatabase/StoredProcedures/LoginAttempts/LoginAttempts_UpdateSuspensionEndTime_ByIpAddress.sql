CREATE PROCEDURE [dbo].[LoginAttempts_UpdateSuspensionEndTime_ByIpAddress]
	@IpAddress nvarchar(50),
	@SuspensionEndTime datetimeoffset(7)
AS
begin
	set nocount on;

	update dbo.[LoginAttempts]
	set SuspensionEndTime = @SuspensionEndTime
	where IpAddress = @IpAddress;
end