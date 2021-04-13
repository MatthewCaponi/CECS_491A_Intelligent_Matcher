CREATE PROCEDURE [dbo].[PublicUserProfile_UpdateHobbies]
	@UserId int,
	@Hobbies nvarchar(1000)
AS
begin
	set nocount on;

	update dbo.[PublicUserProfile]
	set Hobbies = @Hobbies
	where UserId = @UserId;
end
