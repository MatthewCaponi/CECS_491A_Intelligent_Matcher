CREATE PROCEDURE [dbo].[PublicUserProfile_UpdateStatus]
	@UserId int,
	@Status nvarchar(20)
AS
begin
	set nocount on;

	update dbo.[PublicUserProfile]
	set Status = @Status
	where UserId = @UserId;
end
