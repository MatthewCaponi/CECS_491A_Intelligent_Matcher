CREATE PROCEDURE [dbo].[EditTeamAttributes]
	@Id int, 
	@TeamType nvarchar(50),
	@GameType nvarchar(1000),
	@Platform nvarchar(50),
	@Experience nvarchar(50),
	@ListingId int
AS
begin
	set nocount on;

	update dbo.[TeamModel]
	set TeamType= @TeamType, GameType=@GameType,Platform=@Platform,Experience=@Experience
	where   Id=@Id;
end
