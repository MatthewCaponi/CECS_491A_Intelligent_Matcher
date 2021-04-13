CREATE PROCEDURE [dbo].[PublicUserProfile_UpdateHeight]
	@UserId int,
	@Height nvarchar(1000)
AS
begin
	set nocount on;

	update dbo.[PublicUserProfile]
	set Height = @Height
	where UserId = @UserId;
end
