CREATE PROCEDURE [dbo].[ListingSearch_GetAllListings]
	
AS
begin
	set nocount on;
	Select [Id],[Title],[Details],[City],[State],[NumberOfParticipants],
	[InPersonOrRemote],[UserAccountID],[CreationDate]
	from dbo.Listing;
end
