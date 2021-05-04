CREATE PROCEDURE [dbo].[EditParentAttributes]
	@Id int, 
	@Title nvarchar(50),
	@Details nvarchar(1000),
	@City nvarchar(50),
	@State nvarchar(50),
	@NumberOfParticipants int,
	@InPersonOrRemote nvarchar(50)
AS
begin
	set nocount on;

	update dbo.[Listing]
	set Title =@Title, Details=@Details,City=@City,State=@State,NumberOfParticipants=@NumberOfParticipants,
	InPersonOrRemote = @InPersonOrRemote
	where   Id=@Id;
end
