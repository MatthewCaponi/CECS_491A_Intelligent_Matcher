CREATE PROCEDURE [dbo].[PublicUserProfile_UpdatePhoto]
	@UserId int,
	@Photo nvarchar(20)
AS
begin
	set nocount on;

	update dbo.[PublicUserProfile]
	set Photo = @Photo
	where UserId = @UserId;
end
