CREATE PROCEDURE [dbo].[EditRelationshipAttributes]
	@Id int, 
	@RelationshipType nvarchar(50),
	@Age int,
	@Interests nvarchar(50),
	@GenderPreference nvarchar(50),
	@ListingId int
AS
begin
	set nocount on;
	update dbo.[Relationship]
	set RelationshipType=@RelationshipType, Age=@Age,Interests=@Interests,
	GenderPreference=@GenderPreference
	where Id=@Id;
end
