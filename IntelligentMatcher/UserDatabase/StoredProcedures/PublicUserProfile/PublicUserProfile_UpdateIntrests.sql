CREATE PROCEDURE [dbo].[PublicUserProfile_UpdateIntrests]
	@UserId int,
	@Intrests nvarchar(1000)
AS
begin
	set nocount on;

	update dbo.[PublicUserProfile]
	set Intrests = @Intrests
	where UserId = @UserId;
end
