CREATE PROCEDURE [dbo].[UserReports_GetAll]
AS
begin
	set nocount on;
	SELECT [Report], [Date], [ReportingId], [ReportedId]
	from dbo.UserReports
end
