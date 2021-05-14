CREATE PROCEDURE [dbo].[EditParentListing]
	@Id int,
	@Title nvarchar,
	@Details nvarchar,
	@City nvarchar,
	@State nvarchar,
	@NumberOfParticipants int,
	@InpersonOrRemote nvarchar,
	@UserAccountId int
AS
begin
	set nocount on;

	update dbo.[Listing]
	set  Title=@Title,Details=@Details,City=@City,State=@State,
	NumberOfParticipants=@NumberOfParticipants,InPersonOrRemote=@InpersonOrRemote
	where Id = @Id;
end
