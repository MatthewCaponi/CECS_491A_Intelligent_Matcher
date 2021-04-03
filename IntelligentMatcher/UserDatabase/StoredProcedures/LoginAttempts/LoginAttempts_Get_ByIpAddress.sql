CREATE PROCEDURE [dbo].[LoginAttempts_Get_ByIpAddress]
	@IpAddress nvarchar(50)
AS
begin

	set nocount on

	select [Id], [IpAddress], [LoginCounter], [SuspensionEndTime]
	from dbo.[LoginAttempts]
	where IpAddress = @IpAddress;
end