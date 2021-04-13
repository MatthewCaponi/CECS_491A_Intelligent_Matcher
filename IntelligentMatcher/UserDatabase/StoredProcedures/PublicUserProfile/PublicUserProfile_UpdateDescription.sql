CREATE PROCEDURE [dbo].[PublicUserProfile_UpdateDescription]
	@UserId int,
	@Description nvarchar(1000)
AS
begin
	set nocount on;

	update dbo.[PublicUserProfile]
	set Description = @Description
	where UserId = @UserId;
end
