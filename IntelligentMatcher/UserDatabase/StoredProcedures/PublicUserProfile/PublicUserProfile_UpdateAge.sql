CREATE PROCEDURE [dbo].[PublicUserProfile_UpdateAge]
	@UserId int,
	@Age int
AS
begin
	set nocount on;

	update dbo.[PublicUserProfile]
	set Age = @Age
	where UserId = @UserId;
end
