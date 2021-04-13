CREATE PROCEDURE [dbo].[PublicUserProfile_UpdateVisibility]
	@UserId int,
	@Visibility nvarchar(20)
AS
begin
	set nocount on;

	update dbo.[PublicUserProfile]
	set Visibility = @Visibility
	where UserId = @UserId;
end
