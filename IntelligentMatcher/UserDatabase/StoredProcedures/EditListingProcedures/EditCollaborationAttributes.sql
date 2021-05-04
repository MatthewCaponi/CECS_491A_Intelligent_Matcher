CREATE PROCEDURE [dbo].[EditCollaborationAttributes]
	@Id int, 
	@CollaborationType nvarchar(50),
	@InvolvementType nvarchar(1000),
	@Experience nvarchar(50),
	@ListingId int
AS
begin
	set nocount on;

	update dbo.[Collaboration]
	set CollaborationType=@CollaborationType,InvolvementType=@InvolvementType,
	Experience=@Experience
	where   Id=@Id;
end
