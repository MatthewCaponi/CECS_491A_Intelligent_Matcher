CREATE PROCEDURE [dbo].[LoginAttempts_IncrementLoginCounter_ByIpAddress]
	@IpAddress nvarchar(50)
AS
begin
	set nocount on;

	update dbo.[LoginAttempts]
	set LoginCounter = LoginCounter + 1
	where IpAddress = @IpAddress;
end