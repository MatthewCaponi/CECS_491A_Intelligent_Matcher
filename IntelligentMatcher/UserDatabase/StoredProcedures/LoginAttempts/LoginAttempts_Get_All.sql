CREATE PROCEDURE [dbo].[LoginAttempts_Get_All]
AS
begin
	set nocount on;
	SELECT [Id], [IpAddress], [LoginCounter], [SuspensionEndTime]
	from dbo.LoginAttempts;
end