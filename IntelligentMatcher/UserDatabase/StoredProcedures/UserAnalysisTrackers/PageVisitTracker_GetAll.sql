CREATE PROCEDURE [dbo].[PageVisitTracker_GetAll]
AS
begin
	set nocount on;
	Select [PageVisitedName],[PageVisitTime],[UserId]
	from dbo.PageVisitTracker;
end

