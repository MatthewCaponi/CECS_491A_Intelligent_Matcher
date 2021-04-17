CREATE PROCEDURE [dbo].[CreateCollaborationListing]
	@CollaborationType nvarchar(50),
	@InvolvementType nvarchar(50),
	@Experience nvarchar(50),
	@ListingId int,
	@Id int output
AS
begin
	set nocount on;

	insert into dbo.[Collaboration]([CollaborationType],[InvolvementType],
	[Experience],[ListingId])
	values (@CollaborationType,@InvolvementType,@Experience,@ListingId);

	set @Id = SCOPE_IDENTITY();
end
