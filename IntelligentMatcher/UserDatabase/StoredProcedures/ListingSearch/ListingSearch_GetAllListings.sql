CREATE PROCEDURE [dbo].[ListingSearch_GetAllListings]
	
AS
begin
	set nocount on;
	Select [Id],[Title],[Details],[City],[State],[NumberOfParticipants],
	[InPersonOrRemote],[UserAccountID]
	from dbo.Listing;
end

