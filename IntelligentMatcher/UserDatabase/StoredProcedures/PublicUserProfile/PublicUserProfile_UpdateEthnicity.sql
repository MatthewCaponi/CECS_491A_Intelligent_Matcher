CREATE PROCEDURE [dbo].[PublicUserProfile_UpdateEthnicity]
	@UserId int,
	@Ethnicity nvarchar(100)
AS
begin
	set nocount on;

	update dbo.[PublicUserProfile]
	set Ethnicity = @Ethnicity
	where UserId = @UserId;
end
