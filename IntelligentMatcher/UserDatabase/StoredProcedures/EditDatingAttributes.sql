CREATE PROCEDURE [dbo].[EditDatingAttributes]
	@Id int,
	@SexualOrientationPreference nvarchar(50),
	@LookingFor nvarchar(50),
	@ListingId int
AS
begin
	set nocount on;

	update dbo.[Dating]
	set SexualOrientationPreference=@SexualOrientationPreference,
	LookingFor= @LookingFor
	where Id=@Id;
end
