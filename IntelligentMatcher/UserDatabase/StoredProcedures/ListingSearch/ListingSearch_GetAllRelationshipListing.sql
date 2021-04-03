CREATE PROCEDURE [dbo].[ListingSearch_GetAllRelationshipListings]
	
AS
begin
	set nocount on;
	Select [RelationshipType],[Age],[Interests],[GenderPreference],[ListingId]
	from dbo.Relationship;
end


