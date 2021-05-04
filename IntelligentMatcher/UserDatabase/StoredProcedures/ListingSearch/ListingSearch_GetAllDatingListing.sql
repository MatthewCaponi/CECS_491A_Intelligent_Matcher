CREATE PROCEDURE [dbo].[ListingSearch_GetAllDatingListing]

AS
begin
	set nocount on;
	Select [Id],[SexualOrientationPreference],[LookingFor],[ListingId]
	from dbo.Dating;
end

