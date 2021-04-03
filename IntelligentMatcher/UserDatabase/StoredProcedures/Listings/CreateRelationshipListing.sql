CREATE PROCEDURE [dbo].[CreateRelationshipListing]
	@RelationshipType nvarchar(50),
	@Age int,
	@Interests nvarchar(50),
	@GenderPreference nvarchar(50),
	@CreationDate datetime,
	@ListingId int,
	@Id int output
AS
begin
	set nocount on;

	insert into dbo.Relationship([RelationshipType],[Age],[Interests],[GenderPreference],[ListingId])
	values (@RelationshipType,@Age,@Interests,@GenderPreference,@ListingId);

	set @Id = SCOPE_IDENTITY();
end
