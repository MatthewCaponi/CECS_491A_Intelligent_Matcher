CREATE PROCEDURE [dbo].[PublicUserProfile_UpdateGoals]
	@UserId int,
	@Goals nvarchar(1000)
AS
begin
	set nocount on;

	update dbo.[PublicUserProfile]
	set Goals = @Goals
	where UserId = @UserId;
end
