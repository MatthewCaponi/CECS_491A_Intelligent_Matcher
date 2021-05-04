CREATE PROCEDURE [dbo].[CreateDatingListing]
	@SexualOrientationPreference nvarchar(50),
	@LookingFor nvarchar(50),
	@ListingId int,
	@Id int output
AS
begin
	set nocount on;
	insert into dbo.[Dating]([SexualOrientationPreference],[LookingFor],
	[ListingId])
	values (@SexualOrientationPreference,@LookingFor,@ListingId);


	set @Id = SCOPE_IDENTITY();
end
