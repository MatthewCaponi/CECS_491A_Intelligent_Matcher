CREATE PROCEDURE [dbo].[PublicUserProfile_UpdateJobs]
	@UserId int,
	@Jobs nvarchar(1000)
AS
begin
	set nocount on;

	update dbo.[PublicUserProfile]
	set Jobs = @Jobs
	where UserId = @UserId;
end
