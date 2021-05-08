CREATE PROCEDURE [dbo].[SearchTracker_GetAll]
AS
begin
	set nocount on;
	Select [Id],[Search],[SearchTime],[UserId]
	from dbo.ListingSearchTracker;
end

