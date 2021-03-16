CREATE PROCEDURE [dbo].[UserProfile_Get_All]
AS
begin
	set nocount on;
	SELECT [Id], [FirstName], [Surname], [DateOfBirth], [UserAccountId]
	from dbo.[UserProfile];
end
