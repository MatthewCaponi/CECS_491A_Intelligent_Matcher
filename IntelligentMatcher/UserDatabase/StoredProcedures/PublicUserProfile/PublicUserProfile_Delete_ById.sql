CREATE PROCEDURE [dbo].[PublicUserProfile_Delete_ById]
	@Id int
AS
begin
	set nocount on;

	delete 
	from dbo.[PublicUserProfile] 
	where Id = @Id;

end