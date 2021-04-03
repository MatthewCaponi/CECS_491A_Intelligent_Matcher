CREATE PROCEDURE [dbo].[LoginAttempts_ResetLoginCounter_ByIpAddress]
	@IpAddress nvarchar(50)
AS
begin
	set nocount on;

	update dbo.[LoginAttempts]
	set LoginCounter = 0
	where IpAddress = @IpAddress;
end