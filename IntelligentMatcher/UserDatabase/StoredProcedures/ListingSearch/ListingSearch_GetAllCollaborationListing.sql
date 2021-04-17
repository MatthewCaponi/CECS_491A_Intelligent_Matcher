CREATE PROCEDURE [dbo].[ListingSearch_GetAllCollaborationListings]
	
AS
begin
	set nocount on;
	Select [Id],[CollaborationType],[InvolvementType],[Experience],[ListingId]
	from dbo.Collaboration;
end

