CREATE PROCEDURE [dbo].[ListingSearch_GetAllTeamListing]


AS
begin
	set nocount on;
	Select [TeamType],[GameType],[Platform],[Experience],[ListingId]
	from dbo.TeamModel;
end
