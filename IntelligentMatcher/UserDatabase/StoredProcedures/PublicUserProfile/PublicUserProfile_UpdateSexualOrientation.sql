CREATE PROCEDURE [dbo].[PublicUserProfile_UpdateSexualOrientation]
	@UserId int,
	@SexualOrientation nvarchar(100)
AS
begin
	set nocount on;

	update dbo.[PublicUserProfile]
	set SexualOrientation = @SexualOrientation
	where UserId = @UserId;
end
