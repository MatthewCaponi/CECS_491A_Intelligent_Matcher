CREATE PROCEDURE [dbo].[UserProfile_Get_ById]
	@Id int
AS
begin

	set nocount on

	SELECT [Id], [FirstName], [Surname], [DateOfBirth], [UserAccountId]
	from dbo.[UserProfile]
	where Id = @Id;
end