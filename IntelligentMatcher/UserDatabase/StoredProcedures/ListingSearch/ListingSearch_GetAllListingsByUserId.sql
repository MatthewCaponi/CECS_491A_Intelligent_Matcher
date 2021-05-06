CREATE PROCEDURE [dbo].[ListingSearch_GetAllListingsByUserId]
	@UserId int
AS
begin
	set nocount on;
	Select [Id],[Title],[Details],[City],[State],[NumberOfParticipants],
	[InPersonOrRemote],[UserAccountID]
	from dbo.Listing
	WHERE UserAccountID = @UserId;

end



