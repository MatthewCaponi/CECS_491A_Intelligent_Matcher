CREATE PROCEDURE [dbo].[PublicUserProfile_UpdateGender]
	@UserId int,
	@Gender nvarchar(6)
AS
begin
	set nocount on;

	update dbo.[PublicUserProfile]
	set Gender = @Gender
	where UserId = @UserId;
end
