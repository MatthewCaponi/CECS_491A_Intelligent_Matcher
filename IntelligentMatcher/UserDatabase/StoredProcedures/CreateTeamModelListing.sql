CREATE PROCEDURE [dbo].[CreateTeamModelListing]
	@TeamType nvarchar(50),
	@GameType int,
	@Platform nvarchar(50),
	@Experience nvarchar(50),
	@ListingId int,
	@Id int output
AS
begin
	set nocount on;

	insert into dbo.TeamModel([TeamType],[GameType],[Platform],[Experience],[ListingId])
	values (@TeamType,@GameType,@Platform,@Experience,@ListingId);

	set @Id = SCOPE_IDENTITY();
end
